using BarberShop.Communication.RequestDTO.Billings;
using BarberShop.Communication.ResponseDTO.Billings;

namespace BarberShop.Application.UseCases.Billings;
public interface IRegisterNewBilling
{
    Task<ResponseRegisterBillingDTO> Execute(RequestRegisterBillingDTO request);
}
