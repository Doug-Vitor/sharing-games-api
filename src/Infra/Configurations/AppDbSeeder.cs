using Core.Entities;
using Microsoft.EntityFrameworkCore;

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
    List<Genre> genres = [
      new() { Name = "Genre 1" },
      new() { Name = "Genre 2" },
      new() { Name = "Genre 3" }
    ];

    List<Publisher> publishers = [
      new() { Name = "Publisher 1", SiteUrl = "Url 1" },
      new() { Name = "Publisher 2", SiteUrl = "Url 2" },
      new() { Name = "Publisher 3", SiteUrl = "Url 3" }
    ];

    await _dbContext.AddRangeAsync(genres);
    await _dbContext.AddRangeAsync(publishers);
    await _dbContext.SaveChangesAsync();

    List<Game> games = [
      new() { Name="Game 1", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.First().Id },
      new() { Name="Game 2", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.First().Id },
      new() { Name="Game 3", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.ElementAt(1).Id },
      new() { Name="Game 4", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.ElementAt(1).Id },
      new() { Name="Game 5", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.Last().Id },
      new() { Name="Game 6", Synopsis = "", ReleasedAt = new(), PublisherId = publishers.Last().Id }
    ];

    await _dbContext.AddRangeAsync(games);
    await _dbContext.SaveChangesAsync();

    Random random = new();
    List<GameGenre> gameGenres = [];

    foreach (var game in games)
    {
      int genreId = genres.ElementAt(random.Next(0, genres.Count - 1)).Id;
      gameGenres.Add(new() { GenresId = genreId, GamesId = game.Id });
    }

    _dbContext.AddRange(gameGenres);
    await _dbContext.SaveChangesAsync();
  }
}