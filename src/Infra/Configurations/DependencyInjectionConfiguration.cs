using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infra.Configurations;

public static class DependencyInjectionConfiguration
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    => services.AddDatabase();

  static IServiceCollection AddDatabase(this IServiceCollection services)
    => services.AddDbContext<AppDbContext>((sp, options) =>
    {
      if (sp.GetRequiredService<IHostEnvironment>().IsStaging()) options.UseInMemoryDatabase("InMemoryDatabase");
      else options.UseNpgsql(Environment.GetEnvironmentVariable(Constants.DatabaseConnectionString))
                  .UseLazyLoadingProxies();
    });
}