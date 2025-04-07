using AutoMapper;
using BarberShop.Communication.ResponseDTO.Billings;
using BarberShop.Domain.Repositories;

namespace BarberShop.Application.UseCases.Billings.GetAll;
public class GetAllBillings : IGetAllBillings
{
    private readonly IBillingRepository _repository;
    private readonly IMapper _mapper;
    public GetAllBillings(
        IBillingRepository repository,
        IMapper mapper)
    {
        _repository = repository;
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
