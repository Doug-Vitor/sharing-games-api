namespace Core.Entities;

public record class NamedEntity() : BaseEntity()
{
  public string Name { get; init; }
}