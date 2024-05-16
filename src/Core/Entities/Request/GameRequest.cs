using Core.V1.DTOs;

namespace Core.Entities.Request;

public record GameRequest() : BaseRequest()
{
  public string GameUrl { get; init; }
  public int? GameId { get; init; }

  public virtual Game? Game { get; init; }
  public virtual GameRequestAnswer? Answer { get; init; }

  public static implicit operator GameRequest(GameRequestInputModel inputModel) => new()
  {
    Title = inputModel.Title,
    Description = inputModel.Description,
    GameUrl = inputModel.GameUrl,
    UserId = inputModel.UserId
  };
}