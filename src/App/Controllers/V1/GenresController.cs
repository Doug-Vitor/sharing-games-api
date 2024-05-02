using Core.Entities;
using Core.Interfaces;
using Core.V1;
using Core.V1.DTOs;

namespace App.Controllers.V1;

public class GenresController(
  IReadonlySanitizerService<Genre, NamedViewModel> sanitizerService
) : BaseController<Genre, GenreSearchParams, NamedViewModel>(sanitizerService)
{
}
