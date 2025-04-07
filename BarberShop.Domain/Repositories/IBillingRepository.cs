using BarberShop.Domain.Entities;

namespace BarberShop.Domain.Repositories;
public interface IBillingRepository : IGenericRepository<Billing>
{
    Task<List<Billing>> FilterByMonth(DateOnly date);
}
