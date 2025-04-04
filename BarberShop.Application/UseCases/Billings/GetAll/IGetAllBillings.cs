using BarberShop.Communication.ResponseDTO.Billings;

namespace BarberShop.Application.UseCases.Billings.GetAll;
public interface IGetAllBillings
{
    Task<ResponseBillingsDTO> Execute();
}
