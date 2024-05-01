using Core.Interfaces;
using Infra.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infra.Configurations;

public static class DependencyInjectionConfiguration
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    => services.AddTransient<IHttpContextAccessor, HttpContextAccessor>()
               .AddDatabase()
               .AddScoped(typeof(IReadonlyRepository<>), typeof(ReadonlyRepository<>))
               .AddScoped(typeof(IWritableRepository<>), typeof(WritableRepository<>))
               .AddSingleton<AppDbSeeder>();

  static IServiceCollection AddDatabase(this IServiceCollection services)
    => services.AddDbContext<AppDbContext>((sp, options) =>
    {
      options.AddInterceptors(sp.GetServices<IInterceptor>());
      if (sp.GetRequiredService<IHostEnvironment>().IsStaging()) options.UseInMemoryDatabase("InMemoryDatabase");
      else options.UseNpgsql(Environment.GetEnvironmentVariable(Constants.DatabaseConnectionString));
    });
}