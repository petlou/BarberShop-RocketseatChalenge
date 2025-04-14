using BarberShop.Domain.Entities;
using BarberShop.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Persistence;
internal class BarberShopDbContext : DbContext
{
    public BarberShopDbContext(DbContextOptions<BarberShopDbContext> options) : base(options) { }
    public DbSet<Billing> Billings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Transformação de nomes de tabelas e colunas para snake_case
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();

            if (!String.IsNullOrWhiteSpace(tableName))
                entity.SetTableName(tableName.ToSnakeCase());

            foreach (var property in entity.GetProperties())
            {
                var columnName = property.Name;

                if (!String.IsNullOrWhiteSpace(columnName))
                    property.SetColumnName(columnName.ToSnakeCase());
            }
        }
    }
}
