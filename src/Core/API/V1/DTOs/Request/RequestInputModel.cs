namespace Core.V1.DTOs;

public class RequestInputModel : IOwnedByUser
{
  public string? Title { get; set; }
  public string? Description { get; set; }
  public string? UserId { get; set; }
}