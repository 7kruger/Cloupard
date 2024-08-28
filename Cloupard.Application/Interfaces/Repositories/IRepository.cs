namespace Cloupard.Application.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync<TId>(TId id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
    TEntity Add(TEntity entity);
    IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
}