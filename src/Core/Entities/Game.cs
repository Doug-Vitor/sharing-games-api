namespace Core.Entities;

public record Game() : NamedEntity()
{
  public string Synopsis { get; init; }
  public int PublisherId { get; init; }
  public DateOnly ReleasedAt { get; init; }

  public virtual Publisher Publisher { get; init; }
  public virtual ICollection<Image> Images { get; init; }
  public virtual ICollection<Genre> Genres { get; init; }
  public virtual ICollection<Favorite> Favorites { get; init; }
}