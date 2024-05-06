using Cloupard.Domain.Interfaces.Repositories;

namespace Cloupard.DAL.Repositories;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _dbContext;

    public EfRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync<TId>(TId id)
    {
        return await _dbContext.Set<TEntity>().FindAsync([id]);
    }

    public TEntity Create(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        _dbContext.Set<TEntity>().Add(entity);
        
        return entity;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        _dbContext.Set<TEntity>().Add(entity);

        await _dbContext.SaveChangesAsync();
        
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbContext.Set<TEntity>().Update(entity);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbContext.Set<TEntity>().Update(entity);

        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public TEntity Delete(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbContext.Set<TEntity>().Remove(entity);
        
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbContext.Set<TEntity>().Remove(entity);

        await _dbContext.SaveChangesAsync();
        
        return entity;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}