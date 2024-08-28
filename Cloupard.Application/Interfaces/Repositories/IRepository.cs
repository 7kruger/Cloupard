using Ardalis.Specification;

namespace Cloupard.Application.Interfaces.Repositories;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
}