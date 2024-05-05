using System.Net;
using Core.Entities.Request;
using Core.Interfaces;
using Core.V1;
using Core.V1.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers.V1;

public class SuggestionsController(
  IReadonlySanitizerService<Suggestion, RequestViewModel> readonlySanitizerService,
  IAuthenticatedSanitizerService<Suggestion, RequestViewModel, RequestInputModel, RequestUpdateModel> sanitizerService
) : BaseController<Suggestion, SearchParams<Suggestion>, RequestViewModel, RequestInputModel, RequestUpdateModel>(readonlySanitizerService, sanitizerService)
{
  static int _notAllowedStatusCode = (int)HttpStatusCode.MethodNotAllowed;

  public override async Task<IActionResult> Get(int? _) => StatusCode(_notAllowedStatusCode);
  public override async Task<IActionResult> Get(SearchParams<Suggestion>? _) => StatusCode(_notAllowedStatusCode);
  public override async Task<IActionResult> Update(int? id, RequestUpdateModel inputModel) => StatusCode(_notAllowedStatusCode);
  public override async Task<IActionResult> Update(IEnumerable<RequestUpdateModel> inputModels) => StatusCode(_notAllowedStatusCode);
  public override async Task<IActionResult> Remove(int? _) => StatusCode(_notAllowedStatusCode);
  public override async Task<IActionResult> Remove(IEnumerable<int> _) => StatusCode(_notAllowedStatusCode);
}