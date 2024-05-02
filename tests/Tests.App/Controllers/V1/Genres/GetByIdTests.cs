using System.Net;
using Core.Entities;
using Core.Response;
using Core.V1.DTOs;

namespace Tests.App.Controllers.V1.Genres;

public class GetByIdTests() : AuthenticatedBaseTest("/api/v1/genres")
{
  [Fact]
  public async Task WhenUnauthenticatedShouldReturnUnauthorized()
  {
    var response = await Client.GetAsync(BaseAddress);
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Fact]
  public async Task WhenInvalidIdProvidedShouldReturnNotFound()
  {
    await SetupWithAutentication();
    var response = await GetAndParseAsync<ErrorResponse>(BaseAddress + "/0");
    Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
  }

  [Fact]
  public async Task WhenValidIdProvidedShouldReturnTheCorrespondingGame()
  {
    await SetupWithAutentication();
    Genre genre = Context.Set<Genre>().First();

    var response = await GetAndParseAsync<SuccessResponse<NamedViewModel>>($"{BaseAddress}/{genre.Id}");
    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.Equal(genre.Id, response.Data.Id);
    Assert.Equal(genre.Name, response.Data.Name);
  }

}