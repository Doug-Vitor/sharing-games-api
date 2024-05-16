using Core.Entities;
using Core.Entities.Request;
using Core.Enums;

namespace Core.V1.DTOs;

public record GameOfGameRequestViewModel() : NamedEntity()
{
  public static implicit operator GameOfGameRequestViewModel(Game? game)
    => game is null ? null : new()
    {
      Id = game.Id,
      Name = game.Name
    };
}

public class GameRequestViewModel : RequestViewModel
{
  public string? AnswerStatus { get; set; }
  public GameOfGameRequestViewModel? Game { get; set; }

  public static implicit operator GameRequestViewModel(GameRequest request) => new()
  {
    Id = request.Id,
    Title = request.Title,
    Description = request.Description,
    AnswerStatus = (request.Answer?.Status ?? GameRequestAnswerStatus.Pending).ToString(),
    Game = request.Game,
    UserId = request.UserId,
  };
}