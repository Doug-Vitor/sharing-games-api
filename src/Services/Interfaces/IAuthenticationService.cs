using Core.Response;
using Core.V1.DTOs;

namespace Services.Interfaces;

public interface IAuthenticationService
{
  public Task<ActionResponse> GetAuthenticatedUserAsync(int? customStatusCode = null);
  public Task<ActionResponse> SignUpAsync(SignUpInputModel user);
  public Task<ActionResponse> SignInAsync(SignInInputModel user);
  public Task<ActionResponse> SignOutAsync();
}