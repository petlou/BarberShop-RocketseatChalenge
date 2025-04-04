using AutoMapper;
using BarberShop.Application.Utils;
using BarberShop.Communication.RequestDTO.Billings;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Exception;
using BarberShop.Exception.Exceptions;

namespace BarberShop.Application.UseCases.Billings.Update;
public class UpdateBilling : IUpdateBilling
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Billing> _repository;

    public UpdateBilling(
        IGenericRepository<Billing> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Execute(Guid id, RequestRegisterOrUpdateBillingDTO request)
    {
        ValidateBilling.Run(request);

        var billing = await _repository.GetByIdForUpdateAsync(id);

        if (billing is null)
            throw new NotFoundException(ResourceErrorMessages.ENTITY_NOT_FOUND);

        _mapper.Map(request, billing);

        _repository.Update(billing);

        await _unitOfWork.Commit();
    }
}
