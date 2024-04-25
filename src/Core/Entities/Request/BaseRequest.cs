namespace Core.Entities.Request;

public abstract record BaseRequest() : BaseEntity()
{
  public string Title { get; init; }
  public string Description { get; init; }
  public string UserId { get; init; }
}