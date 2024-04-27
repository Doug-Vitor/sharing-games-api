using System.Net;
using System.Net.Http.Json;
using Core.Response;

namespace Tests.App.Controllers;

public class SignOutTests : BaseUserTests
{
  public SignOutTests() : base("/api/Users/SignOut") { }

  [Fact]
  public async Task WhenUnauthenticatedShouldReturnUnauthorized()
  {
    var response = await Client.PostAsJsonAsync(string.Empty, "");
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Fact]
  public async Task WhenAuthenticatedShouldReturnNoContent()
  {
    await Setup();
    await Client.PostAsJsonAsync("/api/Users/SignIn", SignInModel);

    var response = await PostAndParseAsync<ActionResponse>();
    Assert.Equal((int)HttpStatusCode.NoContent, response.StatusCode);
  }
}