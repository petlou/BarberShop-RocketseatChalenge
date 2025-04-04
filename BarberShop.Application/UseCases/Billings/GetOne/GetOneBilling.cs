using AutoMapper;
using BarberShop.Communication.ResponseDTO.Billings;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Exception;
using BarberShop.Exception.Exceptions;

namespace BarberShop.Application.UseCases.Billings.GetOne;
public class GetOneBilling : IGetOneBilling
{
    private readonly IGenericRepository<Billing> _repository;
    private readonly IMapper _mapper;

    public GetOneBilling(
        IGenericRepository<Billing> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseBillingDTO> Execute(Guid id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result is null)
            throw new NotFoundException(ResourceErrorMessages.ENTITY_NOT_FOUND);

        return _mapper.Map<ResponseBillingDTO>(result);
    }
}
