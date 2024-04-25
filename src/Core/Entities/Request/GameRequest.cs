namespace Core.Entities.Request;

public record GameRequest() : BaseRequest()
{
  public string GameUrl { get; init; }
  public virtual GameRequestAnswer Answer { get; init; }
}