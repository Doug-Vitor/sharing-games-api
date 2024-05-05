using System.Net;
using Core.Entities;
using Core.Response;

namespace Tests.App.Controllers.V1.Genres;

public class GetByIdTests() : BaseTest()
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
    var response = await GetAndParseAsync<ErrorResponse>("/0");
    Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
  }

  [Fact]
  public async Task WhenValidIdProvidedShouldReturnTheCorrespondingGame()
  {
    await SetupWithAutentication();
    Genre genre = DbSet.First();

    var response = await GetSingleAndValidateAsync($"/{genre.Id}");
    Assert.Equal(genre.Id, response.Data.Id);
    Assert.Equal(genre.Name, response.Data.Name);
  }
}