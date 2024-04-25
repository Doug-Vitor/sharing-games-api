namespace Core.Entities.Request;

public record GameRequest() : Suggestion()
{
  public string GameUrl { get; init; }
  public virtual GameRequestAnswer Answer { get; init; }
}