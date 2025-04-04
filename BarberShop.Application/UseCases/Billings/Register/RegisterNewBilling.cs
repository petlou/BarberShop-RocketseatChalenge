using AutoMapper;
using BarberShop.Application.Utils;
using BarberShop.Communication.RequestDTO.Billings;
using BarberShop.Communication.ResponseDTO.Billings;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;

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
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseRegisterBillingDTO> Execute(RequestRegisterOrUpdateBillingDTO request)
    {
        ValidateBilling.Run(request);

        var entity = _mapper.Map<Billing>(request);

        await _repository.AddAsync(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterBillingDTO>(entity);
    }
}
