using Domain.Interfaces;
using Domain.Utils;
using Infrastructure.Integrations;
using Infrastructure.Repositories;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
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
    }
}