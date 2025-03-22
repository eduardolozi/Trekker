using Domain.Enums;
using Domain.Interfaces;
using Domain.Utils;
using Duende.IdentityModel.Client;
using Infrastructure.Integrations;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class Ioc
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.ConfigureKeycloak();
    }

    private static void ConfigureKeycloak(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddClientCredentialsTokenManagement()
        .AddClient(TrekkerEnvironment.KcClientId, client =>
        {
            client.ClientId = TrekkerEnvironment.KcClientId;
            client.ClientSecret = TrekkerEnvironment.KcClientSecret;
            client.TokenEndpoint = TrekkerEnvironment.TrekkerTokenEndpoint;
            client.Resource = TrekkerEnvironment.KcClientId;
        });
        
        services.AddHttpClient<IExternalAuthService, KeycloakService>(client =>
        {
            client.BaseAddress = new Uri(TrekkerEnvironment.TrekkerRealmUrl);
        })
        .AddClientCredentialsTokenHandler(TrekkerEnvironment.KcClientId);
        
        services.AddKeycloakAdminHttpClient(options =>
        {
            options.Realm = TrekkerEnvironment.Realm;
            options.Resource = TrekkerEnvironment.KcClientId;
            options.Credentials = new KeycloakClientInstallationCredentials
            {
                Secret = TrekkerEnvironment.KcClientSecret
            };
            options.SslRequired = "none";
            options.AuthServerUrl = TrekkerEnvironment.KcAuthServerUrl;
            options.VerifyTokenAudience = true;
        })
        .AddClientCredentialsTokenHandler(TrekkerEnvironment.KcClientId);
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = TrekkerEnvironment.KcAuthServerUrl + "/realms/" + TrekkerEnvironment.Realm;
                options.Audience = TrekkerEnvironment.KcClientId;
                options.RequireHttpsMetadata = false; // Apenas para desenvolvimento, remova em produção
                options.TokenValidationParameters = new()
                {
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("AccessToken"))
                        {
                            context.Token = context.Request.Cookies["AccessToken"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        
        services.AddKeycloakAuthorization(options =>
        {
            options.VerifyTokenAudience = true;
            options.Realm = TrekkerEnvironment.Realm;
            options.SslRequired = "none";
            options.Resource = TrekkerEnvironment.KcClientId;
            options.VerifyTokenAudience = true;
            options.Credentials = new KeycloakClientInstallationCredentials
            {
                Secret = TrekkerEnvironment.KcClientSecret
            };
            options.AuthServerUrl = TrekkerEnvironment.KcAuthServerUrl;
        })
        .AddAuthorizationBuilder()
        .AddPolicy("Admin", policy =>
        {
            policy.RequireResourceRolesForClient(TrekkerEnvironment.KcClientId, [nameof(UserRoleEnum.Admin)]);
        })
        .AddPolicy("Manager", policy =>
        {
            policy.RequireResourceRolesForClient(TrekkerEnvironment.KcClientId, 
                [nameof(UserRoleEnum.Manager), nameof(UserRoleEnum.Admin)]);
        });
    }
}