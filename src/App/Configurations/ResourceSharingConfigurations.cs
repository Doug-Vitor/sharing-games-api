using System.Net;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;

public static class ResourceSharingConfigurations
{
  internal static IServiceCollection AddResourceSharingLimits(this IServiceCollection services) =>
    services.AddRateLimiter(options =>
    {
      options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;
      options.AddFixedWindowLimiter(Environments.Production, options =>
      {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(20);
      });
    }).AddCors(options =>
    {
      options.AddPolicy(Environments.Development, policy => policy.GetDefaultPolicy("localhost"));
      options.AddPolicy(
        Environments.Production,
        policy => policy.GetDefaultPolicy(Environment.GetEnvironmentVariable(Constants.EnvironmentVarOfClientHost))
      );
    });

  static void GetDefaultPolicy(this CorsPolicyBuilder policy, string hostName)
   => policy.SetIsOriginAllowed(origin => new Uri(origin).Host.Contains(origin))
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .Build();
}