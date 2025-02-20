using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Trekker.Domain.Interfaces;
using Trekker.Domain.Utils;
using Trekker.Infrastructure.Integrations;

namespace Trekker.Infrastructure.IoC;

public static class InfrastructureIoC
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<TrekkerDbContext>();
        services.AddHttpClient<IKeycloakService, KeycloakService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:8080/admin/realms/trekker/");
        });
    }
}