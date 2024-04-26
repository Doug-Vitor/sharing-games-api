using Asp.Versioning;

namespace App.Configurations;

internal static class VersioningConfiguration
{
  internal static IServiceCollection AddVersioning(this IServiceCollection services)
  {
    services.AddApiVersioning(options =>
    {
      options.DefaultApiVersion = new(Constants.LatestApiVersion, 0);
      options.AssumeDefaultVersionWhenUnspecified = true;
      options.ReportApiVersions = true;
      options.ApiVersionReader = new UrlSegmentApiVersionReader();
    });

    return services;
  }
}