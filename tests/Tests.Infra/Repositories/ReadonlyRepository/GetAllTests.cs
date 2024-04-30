using Core.V1;

namespace Tests.Infra.Repositories;

public class GetAllTests : BaseTest<Game>
{
  static readonly SearchParams<Game> _searchParams = new();
  static readonly ICollection<Game> _games = new List<Game>()
  {
    new() { Id = 1, PublisherId = 1 },
    new() { Id = 4, PublisherId = 3 },
    new() { Id = 5, PublisherId = 3 }
  };

  public GetAllTests() : base()
  {
    Repository.Setup(r => r.GetAllAsync(null, null))
              .Returns(async () => _games);

    Repository.Setup(r => r.GetAllAsync(_searchParams, null))
              .Returns(async () => _games.Where(g => g.Id > 3).ToList());
  }

  [Fact]
  public async Task WhenNoSearchParamsProvidedShouldReturnAListOfEntities()
    => Assert.NotEmpty(await Repository.Object.GetAllAsync(null, null));

  [Fact]
  public async Task WhenCursorProvidedShouldPaginateBasedOnTheCursor()
  {
    ICollection<Game> games = await Repository.Object.GetAllAsync(_searchParams, null);
    Assert.NotEmpty(games);
    Assert.True(games.All(g => g.Id > 3));
  }

  [Fact]
  public async Task WhenSearchParamsProvidedShouldFilterResults()
  {
    ICollection<Game> games = await Repository.Object.GetAllAsync(_searchParams, null);
    Assert.NotEmpty(games);
    Assert.True(games.All(g => g.PublisherId == 3));
  }
}