using BarberShop.Domain.Repositories;
using BarberShop.Domain.Repositories.Billings;
using BarberShop.Infrastructure.Persistence;
using BarberShop.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Infrastructure.Extensions;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BarberShopDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Connection")));
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBillingsRepository, BillingsRepository>();
    }
}
