using AutoMapper;
using BarberShop.Communication.RequestDTO.Billings;
using BarberShop.Communication.ResponseDTO.Billings;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Domain.Repositories.Billings;
using BarberShop.Exception.Exceptions;

namespace BarberShop.Application.UseCases.Billings.Register;

public class RegisterNewBilling : IRegisterNewBilling
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBillingsRepository _billingsRepository;
    private readonly IMapper _mapper;

    public RegisterNewBilling(
        IUnitOfWork unitOfWork,
        IBillingsRepository billingsRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _billingsRepository = billingsRepository;
        _mapper = mapper;
    }
    public async Task<ResponseRegisterBillingDTO> Execute(RequestRegisterBillingDTO request)
    {
        Validate(request);

        var entity = _mapper.Map<Billing>(request);

        await _billingsRepository.Add(entity);

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
