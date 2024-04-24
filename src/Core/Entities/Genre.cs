namespace Core.Entities;

public record Genre() : BaseEntity()
{
  public readonly string Name;
  public virtual ICollection<Game> Games { get; init; }
}