using System.Net;
using Core.Entities;
using Core.Response;
using Core.V1.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Tests.App.Controllers.V1.Genres;

public class GetAllTests : AuthenticatedBaseTest
{
  readonly DbSet<Genre> _genres;

  public GetAllTests() : base("/api/v1/genres") => _genres = Context.Set<Genre>();

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

    var response = await GetAndParseAsync<SuccessResponse<IEnumerable<NamedViewModel>>>();
    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.NotEmpty(response.Data);
  }

  [Fact]
  public async Task WhenCursorProvidedShouldPaginateTheResponse()
  {
    await SetupWithAutentication();

    int cursor = _genres.OrderBy(g => g.Id).First().Id + 1;
    var response = await GetAndParseAsync<SuccessResponse<IEnumerable<NamedViewModel>>>($"{BaseAddress}?cursor={cursor}");

    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.NotEmpty(response.Data);
    Assert.True(response.Data.All(game => game.Id > cursor));
  }

  [Fact]
  public async Task WhenNameParameterProvidedShouldFilterTheResponse()
  {
    await SetupWithAutentication();

    string name = _genres.First().Name;
    var response = await GetAndParseAsync<SuccessResponse<IEnumerable<NamedViewModel>>>($"{BaseAddress}?name={name}");

    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.NotEmpty(response.Data);
    Assert.True(response.Data.All(game => game.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)));
  }
}