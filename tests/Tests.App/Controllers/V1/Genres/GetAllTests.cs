using System.Net;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests.App.Controllers.V1.Genres;

public class GetAllTests : BaseTest
{
  readonly DbSet<Genre> _genres;

  public GetAllTests() : base() => _genres = Context.Set<Genre>();

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

    int cursor = _genres.OrderBy(g => g.Id).First().Id + 1;

    var response = await GetManyAndValidateAsync($"?cursor={cursor}");
    Assert.True(response.Data.All(game => game.Id > cursor));
  }

  [Fact]
  public async Task WhenNameParameterProvidedShouldFilterTheResponse()
  {
    await SetupWithAutentication();

    string name = _genres.First().Name;

    var response = await GetManyAndValidateAsync($"?name={name}");
    Assert.True(response.Data.All(game => game.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)));
  }
}