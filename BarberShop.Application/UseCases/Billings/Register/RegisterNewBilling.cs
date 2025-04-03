using AutoMapper;
using BarberShop.Communication.RequestDTO.Billings;
using BarberShop.Communication.ResponseDTO.Billings;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Exception.Exceptions;

namespace BarberShop.Application.UseCases.Billings.Register;

public class RegisterNewBilling : IRegisterNewBilling
{
    private readonly IGenericRepository<Billing> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterNewBilling(
        IGenericRepository<Billing> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseRegisterBillingDTO> Execute(RequestRegisterBillingDTO request)
    {
        Validate(request);

        var entity = _mapper.Map<Billing>(request);

        await _repository.AddAsync(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterBillingDTO>(entity);
    }

    private void Validate(RequestRegisterBillingDTO request)
    {
        var result = new RegisterNewBillingValidator().Validate(request);

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
