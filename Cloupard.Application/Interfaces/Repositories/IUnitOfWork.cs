using Cloupard.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cloupard.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    IRepository<Product> Products { get; }
}