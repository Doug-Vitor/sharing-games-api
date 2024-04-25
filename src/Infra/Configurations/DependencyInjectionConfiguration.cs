using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Configurations;

public static class DependencyInjectionConfiguration
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
  {
    services.AddDbContext<AppDbContext>(options =>
    {
      options.UseNpgsql(Environment.GetEnvironmentVariable(Constants.DatabaseConnectionString))
             .UseLazyLoadingProxies();
    });

    return services;
  }
}