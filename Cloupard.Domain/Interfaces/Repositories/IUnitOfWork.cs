using Cloupard.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cloupard.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task SaveChangesAsync();
    IRepository<Product> ProductsRepository { get; init; }
}