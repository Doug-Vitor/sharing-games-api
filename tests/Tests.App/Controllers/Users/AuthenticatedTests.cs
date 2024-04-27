using System.Net;
using System.Net.Http.Json;
using Core.Response;
using Core.V1.DTOs;

namespace Tests.App.Controllers;

public class AuthenticatedTests : BaseUserTests
{
  const string AuthenticatedUserEndpoint = "Users/Me";
  public AuthenticatedTests() : base() { }

  [Fact]
  public async Task WhenUnauthenticatedShouldReturnUnauthorized()
  {
    var response = await Client.GetAsync(AuthenticatedUserEndpoint);
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Fact]
  public async Task WhenAuthenticatedShouldReturnAuthenticatedUser()
  {
    await Setup();
    await Client.PostAsJsonAsync("Users/SignIn", SignInModel);

    var response = await GetAndParseAsync<SuccessResponse<UserViewModel>>(AuthenticatedUserEndpoint);
    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.Equal(SignInModel.UserName, response.Data.UserName);
  }
}