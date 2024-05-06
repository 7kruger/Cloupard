namespace Cloupard.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    Task<TEntity> GetByIdAsync<TId>(TId id);
    TEntity Create(TEntity entity);
    Task<TEntity> CreateAsync(TEntity entity);
    TEntity Update(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    TEntity Delete(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
    Task SaveChangesAsync();
}