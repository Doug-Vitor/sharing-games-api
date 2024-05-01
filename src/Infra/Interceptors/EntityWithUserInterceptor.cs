using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infra.Interceptors;

internal class EntityWithUserInterceptor(IHttpContextAccessor contextAccessor) : BaseInterceptor()
{
  readonly string _authenticatedUserId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

  protected override void InterceptOperation(DbContext context)
  {
    var entries = context.ChangeTracker.Entries<IOwnedByUser>();

    foreach (var entry in entries)
      entry.Property(e => e.UserId).CurrentValue = _authenticatedUserId;
  }
}
