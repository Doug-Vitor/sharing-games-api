using System.Net;
using Core.Entities;
using Core.Response;

namespace Tests.App.Controllers.V1.Games;

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
    Game game = Context.Set<Game>().First();

    var response = await GetSingleAndValidateAsync($"/{game.Id}");
    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.Equal(game.Name, response.Data.Name);
    Assert.Equal(game.Synopsis, response.Data.Synopsis);
    Assert.Equal(game.PublisherId, response.Data.Publisher.Id);
    Assert.Equal(game.ReleasedAt, response.Data.ReleasedAt);
  }

}