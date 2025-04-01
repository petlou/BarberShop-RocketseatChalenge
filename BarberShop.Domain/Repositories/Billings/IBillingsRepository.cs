using BarberShop.Domain.Entities;

namespace BarberShop.Domain.Repositories.Billings;
public interface IBillingsRepository
{
    Task Add(Billing billing);
}
