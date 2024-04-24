namespace Core.Entities;

public record Publisher() : BaseEntity()
{
  public readonly string Name;
  public readonly string SiteUrl;

  public virtual ICollection<Game> Games { get; init; }
}