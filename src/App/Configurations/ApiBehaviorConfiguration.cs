using System.Text.Json.Serialization;
using Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace App.Configurations;

internal static class ApiBehaviurConfiguration
{
  internal static IServiceCollection AddApiBehavior(this IServiceCollection services)
  {
    services.AddVersioning()
            .AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
              options.InvalidModelStateResponseFactory = ConfigureInvalidModelStateResponseFactory;
            })
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    return services;
  }

  static UnprocessableEntityObjectResult ConfigureInvalidModelStateResponseFactory(ActionContext context)
  {
    var errors = context.ModelState.ToDictionary(
      state => state.Key,
      state => context.ModelState
                      .GetValueOrDefault(state.Key)
                      .Errors
                      .Select(error => error.ErrorMessage)
                      .ToArray()
    );

    return new UnprocessableEntityObjectResult(new ErrorResponse(413, errors));
  }
}