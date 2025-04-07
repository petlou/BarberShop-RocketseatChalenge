using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;

namespace BarberShop.Infrastructure.Persistence.Repositories;
internal class BillingRepository(BarberShopDbContext dbContext) : _GenericRepository<Billing>(dbContext), IBillingRepository
{
    public Task<List<Billing>> FilterByMonth(DateOnly date)
    {
        throw new NotImplementedException();
    }
}
