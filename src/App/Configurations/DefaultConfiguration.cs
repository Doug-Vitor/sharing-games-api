using Infra.Configurations;
using Services.Configurations;

namespace App.Configurations;

internal static class DefaultConfiguration
{
  internal static IServiceCollection AddDefaultConfigurations(this IServiceCollection services)
    => services.AddApiBehavior().AddInfrastructureServices().AddServices();

  internal static WebApplication UseDefaultConfigurations(this WebApplication app)
  {
    app.MapControllers().RequireRateLimiting(app.Environment.EnvironmentName);
    app.UseProductionConfigurations().UseCors().UseAuthentication().UseAuthorization();
    return app;
  }

  internal static WebApplication UseProductionConfigurations(this WebApplication app)
  {
    if (app.Environment.IsProduction())
      app.UseRateLimiter().UseHttpsRedirection();

    return app;
  }
}