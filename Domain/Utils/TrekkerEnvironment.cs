﻿namespace Domain.Utils;

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
    
    public static string TrekkerRealmUrl = Environment.GetEnvironmentVariable("TREKKER_REALM_URL")
        ?? throw new Exception("Environment variable 'TREKKER_REALM_URL' not set.");
    
    public static string TrekkerPostgresConnectionString = Environment.GetEnvironmentVariable("TREKKER_DB_CONNECTION_STRING")
        ?? throw new Exception("Environment variable 'TREKKER_DB_CONNECTION_STRING' not set.");
    
    public static string TrekkerTokenEndpoint = Environment.GetEnvironmentVariable("TREKKER_TOKEN_ENDPOINT")
        ?? throw new Exception("Environment variable 'TREKKER_TOKEN_ENDPOINT' not set.");
    
    public static string RedisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING")
        ?? throw new Exception("Environment variable 'REDIS_CONNECTION_STRING' not set.");
    
    public static string KcClientUuid = Environment.GetEnvironmentVariable("KEYCLOAK_CLIENT_UUID")
        ?? throw new Exception("Environment variable 'KEYCLOAK_CLIENT_UUID' not set.");
    
    public static string AwsAccessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY")
        ?? throw new Exception("Environment variable 'AWS_ACCESS_KEY' not set.");
    
    public static string AwsSecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY")
        ?? throw new Exception("Environment variable 'AWS_SECRET_KEY' not set.");
    
    public static string S3BucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_NAME")
        ?? throw new Exception("Environment variable 'AWS_S3_BUCKET_NAME' not set.");
}