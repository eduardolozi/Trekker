using Microsoft.Extensions.DependencyInjection;
using Trekker.Application.Interfaces;
using Trekker.Application.Services;

namespace Trekker.Application.IoC;

public static class ApplicationIoC
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}