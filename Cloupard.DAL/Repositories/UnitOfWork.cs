using Microsoft.EntityFrameworkCore.Storage;
using Cloupard.Domain.Entities;
using Cloupard.Domain.Interfaces.Repositories;

namespace Cloupard.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext, IRepository<Product> productsRepository)
    {
        _dbContext = dbContext;
        ProductsRepository = productsRepository;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public IRepository<Product> ProductsRepository { get; init; }
}