using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Trekker.Domain.Utils;

namespace Trekker.API.IoC;

public static class ApiIoC
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = TrekkerEnvironment.RedisConnectionString;
        });
        
        services.AddKeycloakWebApiAuthentication(options =>
        {
            options.Audience = TrekkerEnvironment.KcClientId;
            options.Realm = TrekkerEnvironment.Realm;
            options.SslRequired = "none";
            options.Resource = TrekkerEnvironment.KcClientId;
            options.VerifyTokenAudience = true;
            options.Credentials = new KeycloakClientInstallationCredentials
            {
                Secret = TrekkerEnvironment.KcClientSecret
            };
            options.AuthServerUrl = TrekkerEnvironment.KcAuthServerUrl;
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
        });
    }
}