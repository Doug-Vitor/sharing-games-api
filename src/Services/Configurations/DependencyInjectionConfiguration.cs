using Core.Interfaces;
using FluentValidation;
using Infra;
using Infra.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Services.Validators;

namespace Services.Configurations;

public static class DependencyInjectionConfiguration
{
  public static IServiceCollection AddServices(this IServiceCollection services) =>
    services.ConfigureAuthentication()
            .AddTransient<HttpContextAccessor>()
            .AddTransient<Interfaces.IAuthenticationService, Identity.AuthenticationService>()
            .AddValidatorsFromAssemblyContaining<IAppValidator>()
            .AddTransient(typeof(IValidatorService<,>), typeof(ValidatorService<,>))
            .AddScoped(typeof(IReadonlySanitizerService<,>), typeof(ReadonlySanitizerService<,>))
            .AddScoped(typeof(IWritableSanitizerService<,,,>), typeof(WritableSanitizerService<,,,>))
            .AddScoped(typeof(ISanitizerService<,,,>), typeof(SanitizerService<,,,>));

  static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
  {
    services.AddAuthentication().AddBearerToken();
    services.AddAuthorization()
            .AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

    services.ConfigureApplicationCookie(options =>
    {
      options.Events.OnRedirectToLogin = context =>
      {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
      };
    });

    return services;
  }
}