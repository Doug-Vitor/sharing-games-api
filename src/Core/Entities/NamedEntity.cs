namespace Core.Entities;

public abstract record NamedEntity() : BaseEntity()
{
  public string Name { get; init; }
}