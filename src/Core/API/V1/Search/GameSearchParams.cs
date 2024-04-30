using Core.Entities;

namespace Core.V1;

public class GameSearchParams() : SearchParams<Game>()
{
  public string? Name { get; set; }
  public int? PublisherId { get; set; }
  public IEnumerable<int>? GenresIds { get; set; }

  public override void ApplyCustomPredicates() => CustomPredicates = game =>
   (Name == null || Name == string.Empty || game.Name.ToLower().Contains(Name.ToLower()))
    && (PublisherId == null || game.PublisherId == PublisherId)
    && (GenresIds == null || game.Genres.Any(g => GenresIds.Contains(g.Id)));
}