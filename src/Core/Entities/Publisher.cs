namespace Core.Entities;

public record Publisher() : NamedEntity()
{
  public string Name { get; init; }
  public string SiteUrl { get; init; }

  public virtual ICollection<Game> Games { get; init; }
}