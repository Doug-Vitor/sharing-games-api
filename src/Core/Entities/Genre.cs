namespace Core.Entities;

public record Genre() : NamedEntity()
{
  public virtual ICollection<Game> Games { get; init; }
}