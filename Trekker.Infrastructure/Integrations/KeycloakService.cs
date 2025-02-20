using System.Net.Http.Headers;
using System.Net.Http.Json;
using Trekker.Domain.DTOs;
using Trekker.Domain.Interfaces;
using Trekker.Domain.Models;
using Trekker.Domain.Utils;

namespace Trekker.Infrastructure.Integrations;

public class KeycloakService(HttpClient httpClient) : IKeycloakService
{
    private async Task<string> GetClientToken()
    {
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
        return authResponse.access_token;
    }

    public async Task CreateUser(KeycloakRegisterDTO registerDto)
    {
        var clientToken = await GetClientToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);

        var createResponse = await httpClient.PostAsJsonAsync("users", registerDto);
        createResponse.EnsureSuccessStatusCode();
        
        var getIdResponse = await httpClient.GetAsync($"users?exact=true&username={registerDto.Username}");
        getIdResponse.EnsureSuccessStatusCode();
        var createdUser = await getIdResponse.Content.ReadFromJsonAsync<List<KeycloakRegisterDTO>>() ?? throw new Exception("User not found on Keycloak");
        var userId = createdUser[0].Id!;
        
        var roleResponse = await httpClient.GetAsync($"roles");
        roleResponse.EnsureSuccessStatusCode();
        var roles = await roleResponse.Content.ReadFromJsonAsync<List<KeycloakRoleDTO>>() ?? throw new Exception("Error retrieving roles");
        var role = roles.First(x => x.Id == registerDto.RealmRoles[0].ToString());
        
        var x = 3;
    }
}