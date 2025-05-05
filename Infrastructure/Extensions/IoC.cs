using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Domain.Interfaces;
using Domain.Utils;
using Infrastructure.Integrations;
using Infrastructure.Repositories;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class IoC
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<TrekkerContext>();
        services.AddScoped<IExternalAuthService, KeycloakService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
        services.ConfigureAwsS3();
        services.AddScoped<S3Service>();

    }

    private static void ConfigureAwsS3(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonS3>(sp =>
        {
            var credentials = new BasicAWSCredentials(TrekkerEnvironment.AwsAccessKey, TrekkerEnvironment.AwsSecretKey);
            return new AmazonS3Client(credentials, RegionEndpoint.USEast2);
        });
    }
}