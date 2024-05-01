namespace Core.Entities.Request;

public abstract record BaseRequest() : BaseEntity(), IOwnedByUser
{
  public string Title { get; init; }
  public string Description { get; init; }
  public string UserId { get; set; }
}