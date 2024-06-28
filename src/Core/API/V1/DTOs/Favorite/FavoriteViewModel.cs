using Core.Entities;

namespace Core.V1.DTOs;

public class FavoriteViewModel() : ViewModel(), IOwnedByUser
{
  public string UserId { get; set; }
  public GameViewModel Game { get; set; }

  public static implicit operator FavoriteViewModel(Favorite favorite) => new()
  {
    Id = favorite.Id,
    UserId = favorite.UserId,
    Game = favorite.Game,
  };
}