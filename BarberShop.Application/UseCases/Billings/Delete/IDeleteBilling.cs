namespace BarberShop.Application.UseCases.Billings.Delete;
public interface IDeleteBilling
{
    Task Execute(Guid id);
}
