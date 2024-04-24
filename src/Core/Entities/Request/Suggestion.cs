namespace Core.Entities.Request;

public record Suggestion() : BaseEntity()
{
  public readonly string Title;
  public readonly string Description;
  public readonly int UserId;
}