namespace Core.Entities;

public record Genre() : NamedEntity()
{
  public string Name { get; init; }
  public virtual ICollection<Game> Games { get; init; }
}