using System.Net.Http.Json;
using Core.Response;
using Core.V1.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;

namespace Tests.App.Controllers;

public abstract class AuthenticatedBaseTest : BaseTest
{
  protected SignInInputModel SignInModel { get; private set; }
  protected readonly IAuthenticationService AuthService;
  protected UserViewModel AuthenticatedUser { get; private set; }

  public AuthenticatedBaseTest(string baseAddress) : base(baseAddress)
    => AuthService = Factory.Server.Services.GetRequiredService<IAuthenticationService>();

  protected virtual async Task Setup()
  {
    await Seeder.SeedAsync();
    if (SignInModel is null)
    {
      SignInModel = new() { UserName = "MyUserName", Password = "Str0ngP4ss!" };
      await AuthService.SignUpAsync(new() { Email = "fake@email.com", UserName = SignInModel.UserName, Password = SignInModel.Password });
    }
  }

  protected virtual async Task SetupWithAutentication()
  {
    await Setup();
    var response = await Client.PostAsJsonAsync("/api/Users/SignIn", SignInModel);
    AuthenticatedUser = (await ParseAsync<SuccessResponse<UserViewModel>>(response)).Data;
  }
}