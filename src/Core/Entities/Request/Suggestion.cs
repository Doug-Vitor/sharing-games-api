namespace Core.Entities.Request;

public record Suggestion() : BaseEntity()
{
  public string Title { get; init; }
  public string Description { get; init; }
  public string UserId { get; init; }
}