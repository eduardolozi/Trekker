using System.Net.Http.Json;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Domain.Utils;
using Infrastructure.Enums;
using Infrastructure.Extensions;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;

namespace Infrastructure.Integrations;

public class KeycloakService(HttpClient httpClient, IKeycloakUserClient keycloakUserClient) : IExternalAuthService
{
    public async Task<string> RegisterUser(User user)
    {
        var kcUser = new UserRepresentation
        {
            Email = user.Email,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Credentials = new List<CredentialRepresentation>
            {
                new()
                {
                    Type = nameof(CredentialRepresentationsEnum.password),
                    Value = user.Password,
                    Temporary = false
                }
            },
            Enabled = true
        };
        
        var response = await keycloakUserClient.CreateUserWithResponseAsync(TrekkerEnvironment.Realm, kcUser);
        await response.ValidateResponse();
        var userId = response.Headers.Location!.ToString().Split("users/")[1];

        await AssignRole(userId, user.Role.ToString());
        
        var sendEmailResponse = await httpClient.PutAsync($"users/{userId}/send-verify-email", null);
        await sendEmailResponse.ValidateResponse();
        
        return userId;
    }

    public async Task<TokenDTO> Login(LoginDTO login)
    {
        var tokenRequest = new Dictionary<string, string>
        {
            { "grant_type", nameof(GrantTypesEnum.password) },
            { "client_id", TrekkerEnvironment.KcClientId },
            { "client_secret", TrekkerEnvironment.KcClientSecret },
            { "username", login.Username },
            { "password", login.Password }
        };
        
        var form = new FormUrlEncodedContent(tokenRequest);
        var response = await httpClient.PostAsync(TrekkerEnvironment.TrekkerTokenEndpoint, form);
        await response.ValidateResponse();
        return await response.Content.ReadFromJsonAsync<TokenDTO>() ?? throw new Exception("Error retrieving keycloak client token");
    }
    
    public async Task<TokenDTO> RefreshAccessToken(string refreshToken)
    {
        var tokenRequest = new Dictionary<string, string>
        {
            { "grant_type", nameof(GrantTypesEnum.refresh_token) },
            { "client_id", TrekkerEnvironment.KcClientId },
            { "client_secret", TrekkerEnvironment.KcClientSecret },
            { "refresh_token", refreshToken },
        };
        
        var form = new FormUrlEncodedContent(tokenRequest);
        var response = await httpClient.PostAsync(TrekkerEnvironment.TrekkerTokenEndpoint, form);
        await response.ValidateResponse();
        return await response.Content.ReadFromJsonAsync<TokenDTO>() ?? throw new Exception("Error retrieving keycloak client token");
    }
    
    private async Task<KeycloakRoleDTO> GetRole(string roleName)
    {
        var response = await httpClient.GetAsync($"clients/{TrekkerEnvironment.KcClientUuid}/roles/{roleName}");
        await response.ValidateResponse();
        return await response.Content.ReadFromJsonAsync<KeycloakRoleDTO>() ?? throw new Exception("Error retrieving roles");
    }

    private async Task AssignRole(string keycloakUserId, string roleName)
    {
        var role = await GetRole(roleName);
        var response = await httpClient.PostAsJsonAsync($"users/{keycloakUserId}/role-mappings/clients/{TrekkerEnvironment.KcClientUuid}", new List<KeycloakRoleDTO> { role });
        await response.ValidateResponse();
    }
}