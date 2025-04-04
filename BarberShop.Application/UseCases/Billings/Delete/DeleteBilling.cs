using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;

namespace BarberShop.Application.UseCases.Billings.Delete;
public class DeleteBilling : IDeleteBilling
{
    private readonly IGenericRepository<Billing> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBilling(
        IGenericRepository<Billing> repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(Guid id)
    {
        await _repository.DeleteAsync(id);
        await _unitOfWork.Commit();
    }
}
