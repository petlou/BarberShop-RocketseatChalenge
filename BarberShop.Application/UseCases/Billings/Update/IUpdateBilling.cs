using BarberShop.Communication.RequestDTO.Billings;

namespace BarberShop.Application.UseCases.Billings.Update;
public interface IUpdateBilling
{
    Task Execute(Guid id, RequestRegisterOrUpdateBillingDTO request);
}
