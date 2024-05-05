using System.Net;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests.App.Controllers.V1.Games;

public class GetAllTests() : BaseTest()
{
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
    await GetManyAndValidateAsync();
  }

  [Fact]
  public async Task WhenCursorProvidedShouldPaginateTheResponse()
  {
    await SetupWithAutentication();
    int cursor = DbSet.OrderBy(g => g.Id).First().Id + 1;

    var response = await GetManyAndValidateAsync($"?cursor={cursor}");
    Assert.True(response.Data.All(game => game.Id > cursor));
  }

  [Fact]
  public async Task WhenSortParameterProvidedShouldSortTheResponse()
  {
    await SetupWithAutentication();
    string sortBy = nameof(Game.PublisherId);

    var response = await GetManyAndValidateAsync($"?sortBy={sortBy}");
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

    int publisherId = DbSet.First().PublisherId;

    var response = await GetManyAndValidateAsync($"?publisherId={publisherId}");
    Assert.True(response.Data.All(game => game.Publisher.Id == publisherId));
  }

  [Fact]
  public async Task WhenGenresParameterProvidedShouldFilterTheResponse()
  {
    await SetupWithAutentication();
    int genreId = Context.Set<GameGenre>().First().GenresId;

    var response = await GetManyAndValidateAsync($"?genresIds={genreId}");
    Assert.True(response.Data.All(game => game.Genres.ToList().Select(g => g.Id).Contains(genreId)));
  }
}