namespace Core.V1.DTOs;

public class FavoriteInputModel : IOwnedByUser
{
  public int? GameId { get; set; }
  public string? UserId { get; set; }
}