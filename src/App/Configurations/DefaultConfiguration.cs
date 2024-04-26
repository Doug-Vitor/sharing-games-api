using Infra.Configurations;
using Services.Configurations;

namespace App.Configurations;

internal static class DefaultConfiguration
{
  internal static IServiceCollection AddDefaultConfigurations(this IServiceCollection services)
    => services.AddApiBehavior().AddInfrastructureServices().AddServices();

  internal static WebApplication UseDefaultConfigurations(this WebApplication app)
  {
    app.MapControllers();
    app.UseAuthentication().UseAuthorization().UseHttpsRedirection();
    return app;
  }
}