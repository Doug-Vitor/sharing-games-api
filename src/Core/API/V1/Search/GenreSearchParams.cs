using Core.Entities;

namespace Core.V1;

public class GenreSearchParams() : SearchParams<Genre>()
{
  public string? Name { get; set; }
  public int? PublisherId { get; set; }
  public IEnumerable<int>? GenresIds { get; set; }

  public override void ApplyCustomPredicates() => CustomPredicates = genre =>
   Name == null || Name == string.Empty || genre.Name.ToLower().Contains(Name.ToLower());
}