using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Cloupard.Domain.Interfaces;

namespace Cloupard.DAL.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
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
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}