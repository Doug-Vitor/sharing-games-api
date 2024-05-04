using Core.Entities.Request;
using Core.Interfaces;
using Core.V1;
using Core.V1.DTOs;

namespace App.Controllers.V1;

public class SuggestionsController(
  IReadonlySanitizerService<Suggestion, RequestViewModel> readonlySanitizerService,
  IAuthenticatedSanitizerService<Suggestion, RequestViewModel, RequestInputModel, RequestUpdateModel> sanitizerService
) : BaseController<Suggestion, SearchParams<Suggestion>, RequestViewModel, RequestInputModel, RequestUpdateModel>(readonlySanitizerService, sanitizerService)
{
}