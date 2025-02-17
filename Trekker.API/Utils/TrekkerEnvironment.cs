namespace Trekker.API.Utils;

public static class TrekkerEnvironment
{
    public static string Realm = Environment.GetEnvironmentVariable("KEYCLOAK_REALM")
        ?? throw new Exception("Environment variable 'KEYCLOAK_REALM' not set.");
    
    public static string KcClientId = Environment.GetEnvironmentVariable("KEYCLOAK_RESOURCE")
        ?? throw new Exception("Environment variable 'KEYCLOAK_RESOURCE' not set.");
    
    public static string KcClientSecret = Environment.GetEnvironmentVariable("KEYCLOAK_CLIENT_SECRET")
        ?? throw new Exception("Environment variable 'KEYCLOAK_CLIENT_SECRET' not set.");
    
    public static string KcAuthServerUrl = Environment.GetEnvironmentVariable("KEYCLOAK_AUTH_SERVER_URL")
        ?? throw new Exception("Environment variable 'KEYCLOAK_AUTH_SERVER_URL' not set.");
}