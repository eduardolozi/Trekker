using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Distributed;
using Trekker.Domain.DTOs;
using Trekker.Domain.Interfaces;
using Trekker.Domain.Models;
using Trekker.Domain.Utils;

namespace Trekker.Infrastructure.Integrations;

public class KeycloakService(HttpClient httpClient, IDistributedCache cache) : IKeycloakService
{
    private async Task<KeycloakTokenResponse> GetTokenResponseAsync(Dictionary<string, string> tokenRequest)
    {
        var form = new FormUrlEncodedContent(tokenRequest);
        var response = await httpClient.PostAsync("http://localhost:8080/realms/trekker/protocol/openid-connect/token", form);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Keycloak Error: {response.StatusCode} - {errorContent}");
        }
        return await response.Content.ReadFromJsonAsync<KeycloakTokenResponse>() ?? throw new Exception("Error retrieving keycloak client token");
    }
    
    private async Task<string> GetClientToken()
    {
        const string clientTokenKey = "keycloak_client_token";
        var accessToken = await cache.GetStringAsync(clientTokenKey);
        if (accessToken != null)
        {
            return accessToken;
        }
        
        var tokenRequest = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", TrekkerEnvironment.KcClientId },
            { "client_secret", TrekkerEnvironment.KcClientSecret },
        };
        var tokenResponse = await GetTokenResponseAsync(tokenRequest);
        
        await cache.SetStringAsync(clientTokenKey, tokenResponse.access_token, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(tokenResponse.expires_in - 10)
        });
        
        return tokenResponse.access_token;
    }
    
    public Task<KeycloakTokenResponse> GetUserTokens(LoginDTO login)
    {
        var tokenRequest = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", TrekkerEnvironment.KcClientId },
            { "client_secret", TrekkerEnvironment.KcClientSecret },
            { "username", login.Username },
            { "password", login.Password }
        };
        return GetTokenResponseAsync(tokenRequest);
    }

    public Task<KeycloakTokenResponse> RefreshUserToken(string refreshToken)
    {
        var tokenRequest = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "client_id", TrekkerEnvironment.KcClientId },
            { "client_secret", TrekkerEnvironment.KcClientSecret },
            { "refresh_token", refreshToken }
        };
        return GetTokenResponseAsync(tokenRequest);
    }

    public async Task CreateUser(KeycloakRegisterDTO registerDto)
    {
        var clientToken = await GetClientToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);

        var createResponse = await httpClient.PostAsJsonAsync("users", registerDto);
        if (!createResponse.IsSuccessStatusCode)
        {
            var errorContent = await createResponse.Content.ReadAsStringAsync();
            throw new Exception($"Keycloak Error: {createResponse.StatusCode} - {errorContent}");
        }
        
        var createdUser = await GetUserByUsername(registerDto.Username);
        var userId = createdUser.Id!;
        
        await RegisterRole(userId, registerDto.RealmRoles[0].ToString());
    }

    public async Task<List<KeycloakRoleDTO>> GetRoles()
    {
        var clientToken = await GetClientToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);
        var response = await httpClient.GetAsync($"roles");
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Keycloak Error: {response.StatusCode} - {errorContent}");
        }
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
        if (!roleMappingResponse.IsSuccessStatusCode)
        {
            var errorContent = await roleMappingResponse.Content.ReadAsStringAsync();
            throw new Exception($"Keycloak Error: {roleMappingResponse.StatusCode} - {errorContent}");
        }
    }

    public async Task<KeycloakRegisterDTO> GetUserByUsername(string username)
    {
        var clientToken = await GetClientToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);
        var getIdResponse = await httpClient.GetAsync($"users?exact=true&username={username}");
        if (!getIdResponse.IsSuccessStatusCode)
        {
            var errorContent = await getIdResponse.Content.ReadAsStringAsync();
            throw new Exception($"Keycloak Error: {getIdResponse.StatusCode} - {errorContent}");
        }
        var createdUser = await getIdResponse.Content.ReadFromJsonAsync<List<KeycloakRegisterDTO>>() ?? throw new Exception("User not found on Keycloak");
        return createdUser[0];
    }
}