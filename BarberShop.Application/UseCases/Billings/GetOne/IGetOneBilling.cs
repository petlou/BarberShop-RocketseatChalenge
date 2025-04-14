using BarberShop.Communication.ResponseDTO.Billings;

namespace BarberShop.Application.UseCases.Billings.GetOne;
public interface IGetOneBilling
{
    Task<ResponseBillingDTO> Execute(Guid id);
}
