using Cloupard.DAL.Data;
using Cloupard.Domain.Entities;
using Cloupard.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cloupard.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private IRepository<Product> _productRepository;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public IRepository<Product> Products => _productRepository ??= new EfRepository<Product>(_dbContext);
}