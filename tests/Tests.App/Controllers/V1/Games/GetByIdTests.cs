using System.Net;
using Core.Entities;
using Core.Response;
using Core.V1.DTOs;

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
    var response = await GetAndParseAsync<ErrorResponse>(BaseAddress + "/0");
    Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
  }

  [Fact]
  public async Task WhenValidIdProvidedShouldReturnTheCorrespondingGame()
  {
    await SetupWithAutentication();
    Game game = Context.Set<Game>().First();

    var response = await GetAndParseAsync<SuccessResponse<GameViewModel>>($"{BaseAddress}/{game.Id}");
    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    ApplyDefaultAsserts(game, response.Data);
  }

}