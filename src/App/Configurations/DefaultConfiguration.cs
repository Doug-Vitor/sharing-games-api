using Infra.Configurations;

namespace App.Configurations;

internal static class DefaultConfiguration
{
  internal static IServiceCollection AddDefaultConfigurations(this IServiceCollection services)
  {
    services.AddControllers();
    services.AddSwagger();
    services.AddInfrastructureServices();
    return services;
  }

  internal static WebApplication UseDefaultConfigurations(this WebApplication app)
  {
    app.MapControllers();
    app.UseHttpsRedirection();
    return app;
  }
}