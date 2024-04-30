using Core.Entities;
using Core.Interfaces;
using Core.V1;
using Core.V1.DTOs;

namespace App.Controllers.V1;

public class GamesController(
  IReadonlySanitizerService<Game, GameViewModel> sanitizerService
) : BaseController<Game, GameSearchParams, GameViewModel>(sanitizerService, propertyNamesToBeIncluded, propertyNamesToBeIncluded)
{
  static readonly string[] propertyNamesToBeIncluded = [
    nameof(Game.Publisher),
    nameof(Game.Images),
    nameof(Game.Genres)
  ];
}