using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Persistence.Repositories;
internal class BillingRepository(BarberShopDbContext dbContext) : GenericRepository<Billing>(dbContext), IBillingRepository
{
    protected readonly BarberShopDbContext billingContext = dbContext;
    public async Task<List<Billing>> FilterByMonth(DateTime startDate, DateTime endDate)
    {
        return await billingContext
            .Billings
            .AsNoTracking()
            .Where(billing => billing.Date >= startDate && billing.Date <= endDate)
            .OrderBy(billing => billing.Date)
            .ThenBy(billing => billing.Title)
            .ToListAsync();
    }
}
