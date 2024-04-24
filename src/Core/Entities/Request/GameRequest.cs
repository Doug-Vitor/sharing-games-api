namespace Core.Entities.Request;

public record GameRequest() : Suggestion()
{
  public readonly string GameUrl;
}