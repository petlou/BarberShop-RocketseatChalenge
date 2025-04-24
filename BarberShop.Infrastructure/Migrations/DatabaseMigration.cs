using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Infrastructure.Migrations;
public class DatabaseMigration
{
    public async static Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<BarberShopDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
