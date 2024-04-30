using System.Net;
using Core.Entities;
using Core.Response;
using Core.V1.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Tests.App.Controllers.V1;

public class GetAllTests : BaseTest
{
  readonly DbSet<Game> games;

  public GetAllTests() : base() => games = Context.Set<Game>();

  [Fact]
  public async Task WhenUnauthenticatedShouldReturnUnauthorized()
  {
    var response = await Client.GetAsync(BaseAddress);
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Fact]
  public async Task WhenNoSearchParamsProvidedShouldReturnFromFirst()
  {
    await SetupWithAutentication();

    var response = await GetAndParseAsync<SuccessResponse<IEnumerable<GameViewModel>>>();
    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.NotEmpty(response.Data);
  }

  [Fact]
  public async Task WhenCursorProvidedShouldPaginateTheResponse()
  {
    await SetupWithAutentication();

    int cursor = games.OrderBy(g => g.Id).First().Id + 1;
    var response = await GetAndParseAsync<SuccessResponse<IEnumerable<GameViewModel>>>($"{BaseAddress}?cursor={cursor}");

    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.NotEmpty(response.Data);
    Assert.True(response.Data.All(game => game.Id > cursor));
  }

  [Fact]
  public async Task WhenSortParameterProvidedShouldSortTheResponse()
  {
    await SetupWithAutentication();

    string sortBy = nameof(Game.PublisherId);
    var response = await GetAndParseAsync<SuccessResponse<IEnumerable<GameViewModel>>>($"{BaseAddress}?sortBy={sortBy}");

    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.NotEmpty(response.Data);

    for (int i = 0; i < response.Data.Count() - 1; i++)
    {
      if (response.Data.ElementAt(i + 1) is not null)
        Assert.True(response.Data.ElementAt(i).Publisher.Id <= response.Data.ElementAt(i + 1).Publisher.Id);
    }
  }

  [Fact]
  public async Task WhenPublisherIdParameterProvidedShouldFilterTheResponse()
  {
    await SetupWithAutentication();

    int publisherId = games.First().PublisherId;
    var response = await GetAndParseAsync<SuccessResponse<IEnumerable<GameViewModel>>>($"{BaseAddress}?publisherId={publisherId}");

    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.NotEmpty(response.Data);
    Assert.True(response.Data.All(game => game.Publisher.Id == publisherId));
  }

  [Fact]
  public async Task WhenGenresParameterProvidedShouldFilterTheResponse()
  {
    await SetupWithAutentication();

    int genreId = Context.Set<GameGenre>().First().GenresId;
    var response = await GetAndParseAsync<SuccessResponse<IEnumerable<GameViewModel>>>($"{BaseAddress}?genresIds={genreId}");

    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.NotEmpty(response.Data);
    Assert.True(response.Data.All(game => game.Genres.ToList().Select(g => g.Id).Contains(genreId)));
  }
}