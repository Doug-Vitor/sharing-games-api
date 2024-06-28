using Core.V1.DTOs;

namespace Core.Entities;

public record Favorite() : BaseEntity(), IOwnedByUser
{
  public int GameId { get; set; }
  public string UserId { get; set; }

  public virtual Game Game { get; set; }

  public static implicit operator Favorite(FavoriteInputModel inputModel) => new()
  {
    GameId = inputModel.GameId!.Value,
    UserId = inputModel.UserId
  };

  public static implicit operator Favorite(FavoriteUpdateModel inputModel) => new()
  {
    Id = inputModel.Id!.Value,
    GameId = inputModel.GameId!.Value,
    UserId = inputModel.UserId
  };
}