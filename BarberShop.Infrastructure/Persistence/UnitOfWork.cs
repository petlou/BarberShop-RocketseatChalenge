using BarberShop.Domain.Repositories;

namespace BarberShop.Infrastructure.Persistence;
internal class UnitOfWork : IUnitOfWork
{
    private readonly BarberShopDbContext _dbContext;

    public UnitOfWork(BarberShopDbContext context)
    {
        _dbContext = context;
    }
    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
