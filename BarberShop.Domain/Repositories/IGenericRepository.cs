namespace BarberShop.Domain.Repositories;
public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> GetByIdForUpdateAsync(Guid id);
    void Update(T entity);
}
