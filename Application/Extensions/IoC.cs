using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class IoC
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWorkspaceService, WorkspaceService>();
        services.AddScoped<ITeamService, TeamService>();
    }
}