using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Trekker.Domain.DTOs;
using Trekker.Domain.Interfaces;
using Trekker.Domain.Models;
using Trekker.Domain.Utils;

namespace Trekker.Infrastructure.Integrations;

public class KeycloakService(HttpClient httpClient, IDistributedCache cache) : IKeycloakService
{
    private async Task<string> GetClientToken()
    {
        const string clientTokenKey = "keycloak_client_token";
        var jsonData = await cache.GetStringAsync(clientTokenKey);
        if (jsonData != null)
        {
            return jsonData;
        }
        
        var tokenRequest = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", TrekkerEnvironment.KcClientId },
            { "client_secret", TrekkerEnvironment.KcClientSecret },
        };
        var form = new FormUrlEncodedContent(tokenRequest);
        var response = await httpClient.PostAsync("http://localhost:8080/realms/trekker/protocol/openid-connect/token", form);
        response.EnsureSuccessStatusCode();
        var authResponse = await response.Content.ReadFromJsonAsync<KeycloakTokenResponse>() ?? throw new Exception("Error retrieving keycloak client token");
        
        await cache.SetStringAsync(clientTokenKey, authResponse.access_token, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(authResponse.expires_in - 10)
        });
        
        return authResponse.access_token;
    }

    public async Task CreateUser(KeycloakRegisterDTO registerDto)
    {
        var clientToken = await GetClientToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);

        var createResponse = await httpClient.PostAsJsonAsync("users", registerDto);
        createResponse.EnsureSuccessStatusCode();
        
        var createdUser = await GetUserByUsername(registerDto.Username);
        var userId = createdUser.Id!;
        
        await RegisterRole(userId, registerDto.RealmRoles[0].ToString());
    }

    public async Task<List<KeycloakRoleDTO>> GetRoles()
    {
        var clientToken = await GetClientToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);
        var response = await httpClient.GetAsync($"roles");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<KeycloakRoleDTO>>() ?? throw new Exception("Error retrieving roles");
    }

    public async Task RegisterRole(string keycloakUserId, string roleName)
    {
        var clientToken = await GetClientToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);
        var roles = await GetRoles();
        var assignedRole = roles.First(x => x.Name == roleName);
        
        var roleMappingResponse = await httpClient.PostAsJsonAsync($"users/{keycloakUserId}/role-mappings/realm", new List<KeycloakRoleDTO> { assignedRole });
        roleMappingResponse.EnsureSuccessStatusCode();
    }

    public async Task<KeycloakRegisterDTO> GetUserByUsername(string username)
    {
        var clientToken = await GetClientToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);
        var getIdResponse = await httpClient.GetAsync($"users?exact=true&username={username}");
        getIdResponse.EnsureSuccessStatusCode();
        var createdUser = await getIdResponse.Content.ReadFromJsonAsync<List<KeycloakRegisterDTO>>() ?? throw new Exception("User not found on Keycloak");
        return createdUser[0];
    }
}