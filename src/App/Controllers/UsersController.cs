using Core.V1.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace App.Controllers;

public class UsersController : BaseController
{
  readonly IAuthenticationService _authenticationService;
  public UsersController(IAuthenticationService authenticationService)
    => _authenticationService = authenticationService;

  [HttpGet("Me")]
  public async Task<IActionResult> GetAuthenticatedUser()
  {
    var result = await _authenticationService.GetAuthenticatedUserAsync();
    return StatusCode(result.StatusCode, result);
  }

  [AllowAnonymous]
  public async Task<IActionResult> SignUp(SignUpInputModel user)
  {
    var result = await _authenticationService.SignUpAsync(user);
    return StatusCode(result.StatusCode, result);
  }

  [AllowAnonymous]
  [HttpPost("SignIn")]
  public async Task<IActionResult> SignIn(SignInInputModel user)
  {
    var result = await _authenticationService.SignInAsync(user);
    return StatusCode(result.StatusCode, result);
  }

  [HttpPost("SignOut")]
  public new async Task<IActionResult> SignOut()
  {
    var result = await _authenticationService.SignOutAsync();
    return StatusCode(result.StatusCode, result);
  }
}