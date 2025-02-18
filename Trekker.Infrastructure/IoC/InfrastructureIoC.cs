using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Trekker.Domain.Utils;

namespace Trekker.Infrastructure.IoC;

public static class InfrastructureIoC
{
    public static void UseInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<TrekkerDbContext>();
    }
}