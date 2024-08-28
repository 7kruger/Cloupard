using Cloupard.Application.Interfaces.Services;
using Cloupard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Cloupard.DAL.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public SoftDeleteInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var dbContext = eventData.Context;
    
        if (dbContext == null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        
        var entries = dbContext.ChangeTracker.Entries<ISoftDeletable>()
            .Where(x => x.State == EntityState.Deleted);
    
        foreach (var entry in entries)
        {
            entry.State = EntityState.Modified;
            entry.Property(x => x.IsDeleted).CurrentValue = true;
            entry.Property(x => x.DeletedAt).CurrentValue = DateTime.UtcNow;
            entry.Property(x => x.DeletedBy).CurrentValue = _currentUserService.UserId;
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}