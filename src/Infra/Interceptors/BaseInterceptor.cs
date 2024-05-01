using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infra.Interceptors;

internal abstract class BaseInterceptor : SaveChangesInterceptor, IInterceptor
{
  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
      DbContextEventData eventData,
      InterceptionResult<int> result,
      CancellationToken cancellationToken = default)
  {
    if (eventData.Context is not null)
      InterceptOperation(eventData.Context);

    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  protected abstract void InterceptOperation(DbContext context);
}
