using BarberShop.Domain.Repositories;

namespace BarberShop.Infrastructure.Persistence.Repositories;
internal class UnitOfWork(BarberShopDbContext context) : IUnitOfWork
{
    public async Task Commit() => await context.SaveChangesAsync();
}
