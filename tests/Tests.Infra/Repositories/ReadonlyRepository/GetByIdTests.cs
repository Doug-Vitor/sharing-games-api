namespace Tests.Infra.Repositories;

public class GetByIdTests : BaseTest<Game>
{
  static readonly Game _game = new() { Id = 1, PublisherId = 1 };

  public GetByIdTests() : base()
  {
    Repository.Setup(r => r.GetByIdAsync(null)).Throws<ArgumentNullException>();
    Repository.Setup(r => r.GetByIdAsync(1)).Returns(async () => _game);
  }

  [Fact]
  public async Task WhenNullIdProvidedShouldThrowException()
    => await Assert.ThrowsAsync<ArgumentNullException>(() => Repository.Object.GetByIdAsync(null));

  [Fact]
  public async Task WhenIdProvidedShouldReturnTheCorrespondingGame()
  {
    int id = 1;
    Game game = await Repository.Object.GetByIdAsync(id);
    Assert.NotNull(game);
    Assert.True(id == game.Id);
  }
}