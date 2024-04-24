namespace Core.Entities;

public record Game() : BaseEntity()
{
  public readonly string Name;
  public readonly string Synopsis;
  public readonly int PublisherId;
  public readonly int RequestedByUserId;
  public readonly DateOnly ReleasedAt;

  public virtual Publisher Publisher { get; init; }
  public virtual ICollection<Image> Images { get; init; }
  public virtual ICollection<Genre> Genres { get; init; }
}