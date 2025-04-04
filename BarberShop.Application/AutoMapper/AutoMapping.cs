using AutoMapper;
using BarberShop.Communication.RequestDTO.Billings;
using BarberShop.Communication.ResponseDTO.Billings;
using BarberShop.Domain.Entities;

namespace BarberShop.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterOrUpdateBillingDTO, Billing>();
    }

    private void EntityToResponse()
    {
        CreateMap<Billing, ResponseRegisterBillingDTO>();
        CreateMap<Billing, ResponseShortBillingDTO>();
        CreateMap<Billing, ResponseBillingDTO>();
    }
}
