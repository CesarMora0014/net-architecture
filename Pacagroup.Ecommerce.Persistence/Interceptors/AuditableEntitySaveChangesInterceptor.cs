﻿

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pacagroup.Ecommerce.Application.Interface.Presentation;
using Pacagroup.Ecommerce.Domain.Common;

namespace Pacagroup.Ecommerce.Persistence.Interceptors;

public class AuditableEntitySaveChangesInterceptor: SaveChangesInterceptor
{
    private readonly ICurrentUser currentUser;

    public AuditableEntitySaveChangesInterceptor(ICurrentUser currentUser)
    {
        this.currentUser = currentUser;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext context)
    {
        if (context == null) return;

        foreach(var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if(entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = currentUser.UserName;
                entry.Entity.Created = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy = currentUser.UserName;
                entry.Entity.LastModified = DateTime.Now;
            }
        }
    }
}
