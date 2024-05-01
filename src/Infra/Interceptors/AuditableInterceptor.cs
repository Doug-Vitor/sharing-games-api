using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Interceptors;

internal class AuditableInterceptor : BaseInterceptor
{
  protected override void InterceptOperation(DbContext context)
  {
    DateTime utcNow = DateTime.UtcNow;
    var entries = context.ChangeTracker
                         .Entries<BaseEntity>()
                         .Where(entry => entry.State == EntityState.Modified);

    foreach (var entry in entries)
      entry.Property(e => e.UpdatedAt).CurrentValue = utcNow;
  }
}