using Core.Entities.Request;
using Core.Interfaces;
using Core.V1;
using Core.V1.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Sanitizers;

namespace App.Controllers.V1;

public class GameRequestsController(
  IReadonlySanitizerService<GameRequest, GameRequestViewModel> readonlySanitizerService,
  GameRequestSanitizerService sanitizerService
) : BaseController<GameRequest, GameRequestSearchParams, GameRequestViewModel, GameRequestInputModel, GameRequestUpdateModel>(
  readonlySanitizerService,
  sanitizerService,
  propertyNamesToBeIncluded,
  propertyNamesToBeIncluded
)
{
  static readonly string[] propertyNamesToBeIncluded = [nameof(GameRequest.Answer)];

  public override async Task<IActionResult> Get([FromQuery] GameRequestSearchParams? searchParams)
  {
    var response = await (SanitizerService as GameRequestSanitizerService).GetAllAsync(searchParams, propertyNamesToBeIncluded);
    return StatusCode(response.StatusCode, response);
  }
}