using System.Net;
using Core.Response;
using Core.V1.DTOs;

namespace Tests.App.Controllers;

public class SignInTests : BaseUserTests
{
  public SignInTests() : base("/api/Users/SignIn") { }

  [Fact]
  public async Task WhenNoUserNameProvidedShouldNotSignIn()
  {
    SignInInputModel signInModel = new() { UserName = "" };
    var response = await PostAndParseAsync<ErrorResponse>(signInModel);
    Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
  }

  [Fact]
  public async Task WhenInvalidPasswordProvidedShouldNotSignIn()
  {
    await Setup();
    SignInInputModel signInModel = new() { UserName = SignInModel.UserName, Password = SignInModel.Password + "___" };
    var response = await PostAndParseAsync<ErrorResponse>(signInModel);
    Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
  }

  [Fact]
  public async Task WhenValidBodyProvidedShouldSignIn()
  {
    await Setup();
    var response = await PostAndParseAsync<SuccessResponse<UserViewModel>>(SignInModel);
    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.Equal(SignInModel.UserName, response.Data.UserName);
  }
}