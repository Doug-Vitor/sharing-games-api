using Core.Entities.Request;
using Core.Enums;

namespace Core.V1.DTOs;

public class GameRequestViewModel : RequestViewModel
{
  public string? AnswerStatus { get; set; }
  public GameViewModel? Game { get; set; }

  public static implicit operator GameRequestViewModel(GameRequest request) => new()
  {
    Id = request.Id,
    Title = request.Title,
    Description = request.Description,
    AnswerStatus = (request.Answer?.Status ?? GameRequestAnswerStatus.Pending).ToString(),
    Game = null, // temp
  };
}