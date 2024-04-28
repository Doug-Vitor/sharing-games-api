namespace Core.Entities;

public abstract record BaseEntity()
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}