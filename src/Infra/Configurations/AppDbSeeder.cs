using Core.Entities;
using Core.Entities.Request;
using Core.Enums;
using Infra.Entities;

namespace Infra.Configurations;

public class AppDbSeeder(AppDbContext dbContext)
{
  readonly AppDbContext _dbContext = dbContext;

  public async Task SeedAsync()
  {
    if (_dbContext.Set<Genre>().Any() || _dbContext.Set<Publisher>().Any()) return;
    await SeedTables();
  }

  async Task SeedTables()
  {
    var genres = AddGenres();
    var publishers = AddPublishers();
    var users = AddUsers();

    await Task.WhenAll(genres, publishers, users);
    await _dbContext.SaveChangesAsync();

    var games = await SeedGames(publishers.Result);
    await SeedGameGenres(games, genres.Result);

    var requests = await SeedGameRequests(users.Result);
    await SeedGameRequestAnswers(requests);
  }

  async Task<IEnumerable<Genre>> AddGenres()
  {
    IEnumerable<Genre> genres = [
      new() { Name = "Genre 1" },
      new() { Name = "Genre 2" },
      new() { Name = "Genre 3" }
    ];

    await _dbContext.AddRangeAsync(genres);
    return genres;
  }

  async Task<IEnumerable<Publisher>> AddPublishers()
  {
    IEnumerable<Publisher> publishers = [
      new() { Name = "Publisher 1", SiteUrl = "Url 1" },
      new() { Name = "Publisher 2", SiteUrl = "Url 2" },
      new() { Name = "Publisher 3", SiteUrl = "Url 3" }
    ];

    await _dbContext.AddRangeAsync(publishers);
    return publishers;
  }

  async Task<IEnumerable<Game>> SeedGames(IEnumerable<Publisher> publishers)
  {
    IEnumerable<Game> games = [
      new() { Name="Game 1", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.First().Id },
      new() { Name="Game 2", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.First().Id },
      new() { Name="Game 3", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.ElementAt(1).Id },
      new() { Name="Game 4", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.ElementAt(1).Id },
      new() { Name="Game 5", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.Last().Id },
      new() { Name="Game 6", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.Last().Id }
    ];

    await _dbContext.AddRangeAsync(games);
    await _dbContext.SaveChangesAsync();
    return games;
  }

  async Task SeedGameGenres(IEnumerable<Game> games, IEnumerable<Genre> genres)
  {
    Random random = new();
    List<GameGenre> gameGenres = [];

    foreach (var game in games)
    {
      int genreId = genres.ElementAt(random.Next(0, genres.Count() - 1)).Id;
      gameGenres.Add(new() { GenresId = genreId, GamesId = game.Id });
    }

    _dbContext.AddRange(gameGenres);
    await _dbContext.SaveChangesAsync();
  }

  async Task<IEnumerable<User>> AddUsers()
  {
    IEnumerable<User> users = [
      new() { UserName = "User 1", Email = "user1@sharing-games.com" },
      new() { UserName = "User 2", Email = "user2@sharing-games.com" },
      new() { UserName = "User 3", Email = "user3@sharing-games.com" },
    ];

    await _dbContext.AddRangeAsync(users);
    return users;
  }

  async Task<IEnumerable<GameRequest>> SeedGameRequests(IEnumerable<User> users)
  {
    List<GameRequest> requests = [];

    foreach (var user in users)
      for (int i = 0; i < 2; i++)
        requests.Add(new()
        {
          Title = "This is an awesome game",
          Description = "Please, add this for me",
          GameUrl = "sharing-games.com",
          UserId = user.Id
        });

    await _dbContext.AddRangeAsync(requests);
    await _dbContext.SaveChangesAsync();
    return requests;
  }

  async Task SeedGameRequestAnswers(IEnumerable<GameRequest> requests)
  {
    var dbSet = _dbContext.Set<GameRequestAnswer>();

    foreach (var groupedRequest in requests.GroupBy(r => r.UserId))
      for (int i = 0; i < groupedRequest.ToList().Count; i++)
        await dbSet.AddAsync(new()
        {
          Status = i == 0 ? GameRequestAnswerStatus.Accepted : GameRequestAnswerStatus.Pending,
          GameRequestId = groupedRequest.ElementAt(i).Id
        });

    await _dbContext.SaveChangesAsync();
  }
}