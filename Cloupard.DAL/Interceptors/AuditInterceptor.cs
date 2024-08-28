using Cloupard.Application.Interfaces.Services;
using Cloupard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Cloupard.DAL.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public AuditInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
    
        if (dbContext == null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        
        var entries = dbContext.ChangeTracker.Entries<IAuditable>()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
    
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                entry.Property(x => x.CreatedBy).CurrentValue = _currentUserService.UserId;
            }
    
            if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                entry.Property(x => x.UpdatedBy).CurrentValue = _currentUserService.UserId;
            }
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}