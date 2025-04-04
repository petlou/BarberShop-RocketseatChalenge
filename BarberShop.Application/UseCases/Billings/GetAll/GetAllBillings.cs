using AutoMapper;
using BarberShop.Communication.ResponseDTO.Billings;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;

namespace BarberShop.Application.UseCases.Billings.GetAll;
public class GetAllBillings : IGetAllBillings
{
    private readonly IGenericRepository<Billing> _repository;
    private readonly IMapper _mapper;
    public GetAllBillings(
        IGenericRepository<Billing> billingRepository,
        IMapper mapper)
    {
        _repository = billingRepository;
        _mapper = mapper;
    }
    public async Task<ResponseBillingsDTO> Execute()
    {
        var result = await _repository.GetAllAsync();

        return new ResponseBillingsDTO
        {
            Billings = _mapper.Map<List<ResponseShortBillingDTO>>(result)
        };
    }
}
