using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Cloupard.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cloupard.DAL.Repositories;

public class EfRepository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity> where TEntity : class
{
    public EfRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public EfRepository(DbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext, specificationEvaluator)
    {
    }
}