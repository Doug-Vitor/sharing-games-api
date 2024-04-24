namespace App.Configurations;

internal static class SwaggerConfiguration
{
  internal static IServiceCollection AddSwagger(this IServiceCollection services)
  {
    services = services.AddEndpointsApiExplorer().AddSwaggerGen();
    return services;
  }

  internal static WebApplication AddSwagger(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    return app;
  }
}