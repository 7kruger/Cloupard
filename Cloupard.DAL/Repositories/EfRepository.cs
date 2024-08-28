using Cloupard.DAL.Data;
using Cloupard.Application.Interfaces.Repositories;

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

    public async Task<TEntity?> GetByIdAsync<TId>(TId id)
    {
        return await _dbContext.Set<TEntity>().FindAsync([id]);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        _dbContext.Set<TEntity>().Add(entity);

        await _dbContext.SaveChangesAsync();
        
        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        _dbContext.Set<TEntity>().AddRange(entities);

        await _dbContext.SaveChangesAsync();
        
        return entities;
    }

    public TEntity Add(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        _dbContext.Set<TEntity>().Add(entity);
        
        return entity;
    }

    public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        _dbContext.Set<TEntity>().AddRange(entities);
        
        return entities;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbContext.Set<TEntity>().Update(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        
        _dbContext.Set<TEntity>().UpdateRange(entities);

        await _dbContext.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbContext.Set<TEntity>().Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        
        _dbContext.Set<TEntity>().UpdateRange(entities);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbContext.Set<TEntity>().Remove(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        
        _dbContext.Set<TEntity>().RemoveRange(entities);

        await _dbContext.SaveChangesAsync();
    }

    public void Delete(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbContext.Set<TEntity>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        
        _dbContext.Set<TEntity>().RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}