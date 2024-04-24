namespace Core.Entities;

public abstract record BaseEntity()
{
  public readonly int Id;
  public readonly DateTime CreatedAt, UpdatedAt;
}