using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories.Billings;

namespace BarberShop.Infrastructure.Persistence.Repositories;
internal class BillingsRepository : IBillingsRepository
{
    private readonly BarberShopDbContext _dbContext;
    public BillingsRepository(BarberShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Billing billing)
    {
        await _dbContext.Billings.AddAsync(billing);
    }
}
