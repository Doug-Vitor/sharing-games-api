using Core.V1.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;

namespace Tests.App.Controllers;

public abstract class BaseUserTests : BaseTest
{
  protected SignInInputModel SignInModel { get; private set; }
  protected readonly IAuthenticationService AuthService;

  public BaseUserTests(string? baseAddress = "/api/Users") : base(baseAddress)
    => AuthService = Factory.Server.Services.GetRequiredService<IAuthenticationService>();

  protected async Task Setup()
  {
    if (SignInModel is null)
    {
      SignInModel = new() { UserName = "MyUserName", Password = "Str0ngP4ss!" };
      await AuthService.SignUpAsync(new() { Email = "fake@email.com", UserName = SignInModel.UserName, Password = SignInModel.Password });
    }
  }
}