using Core.Entities;
using Core.Interfaces;
using Core.V1.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Sanitizers;

namespace App.Controllers.V1;

public class FavoritesController(
  IReadonlySanitizerService<Favorite, FavoriteViewModel> readonlySanitizerService,
  FavoriteSanitizerService sanitizerService
) : BaseController<Favorite, FavoriteSearchParams, FavoriteViewModel, FavoriteInputModel, FavoriteUpdateModel>(
  readonlySanitizerService,
  sanitizerService,
  propertyNamesToBeIncluded,
  propertyNamesToBeIncluded
)
{
  static readonly string[] propertyNamesToBeIncluded = [
    nameof(Favorite.Game),
    "Game.Publisher",
    "Game.Genres",
    "Game.Images"
  ];

  [HttpGet]
  public override async Task<IActionResult> Get([FromQuery] FavoriteSearchParams? searchParams)
  {
    var response = await (SanitizerService as FavoriteSanitizerService).GetAllAsync(searchParams, propertyNamesToBeIncluded);
    return StatusCode(response.StatusCode, response);
  }
}