using BarberShop.Domain.Repositories;
using BarberShop.Exception;
using BarberShop.Exception.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Persistence.Repositories;
internal class _GenericRepository<T>(BarberShopDbContext dbContext) : IGenericRepository<T> where T : class
{
    protected readonly BarberShopDbContext _dbContext = dbContext;
    protected readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id) ?? throw new NotFoundException(ResourceErrorMessages.ENTITY_NOT_FOUND);
        _dbSet.Remove(entity);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }

    public async Task<T?> GetByIdForUpdateAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}
