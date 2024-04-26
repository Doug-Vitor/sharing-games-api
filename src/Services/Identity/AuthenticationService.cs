using Core.Response;
using Core.V1.DTOs;
using Infra.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using System.Net;

namespace Services.Identity;

public class AuthenticationService : IAuthenticationService
{
  readonly UserManager<User> _userManager;
  readonly SignInManager<User> _signInManager;
  readonly HttpContextAccessor _contextAccessor;

  public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, HttpContextAccessor contextAccessor)
    => (_userManager, _signInManager, _contextAccessor) = (userManager, signInManager, contextAccessor);

  public async Task<ActionResponse> GetAuthenticatedUserAsync(int? customStatusCode = null)
  {
    if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
    {
      User authenticatedUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
      System.Console.WriteLine(new UserViewModel("hahaha").ToJson());
      return new SuccessResponse<UserViewModel>(
        customStatusCode ?? (int)HttpStatusCode.OK,
        new(authenticatedUser.UserName)
      );
    }

    return new((int)HttpStatusCode.Unauthorized);
  }

  public async Task<ActionResponse> SignUpAsync(SignUpInputModel user)
  {
    var result = await _userManager.CreateAsync(user, user.Password);

    if (result.Succeeded) return await GetAuthenticatedUserAsync((int)HttpStatusCode.Created);
    return new ErrorResponse(
      (int)HttpStatusCode.UnprocessableEntity,
      result.Errors.ToDictionary(err => err.Code, err => new string[] { err.Description })
    );
  }

  public async Task<ActionResponse> SignInAsync(SignInInputModel user)
  {
    var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);

    if (result.Succeeded) return await GetAuthenticatedUserAsync();
    return new((int)HttpStatusCode.BadRequest);
  }

  public async Task<ActionResponse> SignOutAsync()
  {
    await _signInManager.SignOutAsync();
    return new((int)HttpStatusCode.NoContent);
  }
}
