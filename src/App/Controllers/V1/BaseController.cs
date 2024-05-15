using Asp.Versioning;
using Core.Entities;
using Core.Interfaces;
using Core.V1;
using Core.V1.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers.V1;

[ApiVersion(Constants.LatestApiVersion)]
public abstract class BaseController : VersionedBaseController;

public abstract class BaseController<T, TSearch, TViewModel>(
  IReadonlySanitizerService<T, TViewModel> sanitizerService,
  IEnumerable<string>? propertyNamesToBeIncludedOnGetAction = null,
  IEnumerable<string>? propertyNamesToBeIncludedOnIndexAction = null
) : BaseController where T : BaseEntity where TSearch : SearchParams<T> where TViewModel : ViewModel
{
  protected readonly IReadonlySanitizerService<T, TViewModel> ReadonlySanitizerService = sanitizerService;
  readonly IEnumerable<string>? _propertyNamesToBeIncludedOnGetAction = propertyNamesToBeIncludedOnGetAction;
  readonly IEnumerable<string>? _propertyNamesToBeIncludedOnIndexAction = propertyNamesToBeIncludedOnIndexAction;

  [HttpGet("{id}")]
  public virtual async Task<IActionResult> Get(int? id)
  {
    var response = await ReadonlySanitizerService.GetByIdAsync(id, _propertyNamesToBeIncludedOnGetAction);
    return StatusCode(response.StatusCode, response);
  }

  [HttpGet]
  public virtual async Task<IActionResult> Get([FromQuery] TSearch? searchParams)
  {
    var response = await ReadonlySanitizerService.GetAllAsync(searchParams, _propertyNamesToBeIncludedOnIndexAction);
    return StatusCode(response.StatusCode, response);
  }
}

public abstract class BaseController<T, TSearch, TViewModel, TInputModel, TUpdateModel>(
  IReadonlySanitizerService<T, TViewModel> readonlySanitizerService,
  ISanitizerService<T, TViewModel, TInputModel, TUpdateModel> sanitizerService,
  IEnumerable<string>? propertyNamesToBeIncludedOnGetAction = null,
  IEnumerable<string>? propertyNamesToBeIncludedOnIndexAction = null
) : BaseController<T, TSearch, TViewModel>(
  readonlySanitizerService,
  propertyNamesToBeIncludedOnGetAction,
  propertyNamesToBeIncludedOnIndexAction
) where T : BaseEntity where TSearch : SearchParams<T> where TViewModel : ViewModel where TInputModel : class where TUpdateModel : class, IKeyed
{
  protected readonly ISanitizerService<T, TViewModel, TInputModel, TUpdateModel> SanitizerService = sanitizerService;

  [HttpPost]
  public virtual async Task<IActionResult> Insert(TInputModel inputModel)
  {
    var response = await SanitizerService.InsertAsync(inputModel);
    return StatusCode(response.StatusCode, response);
  }

  [HttpPost("BatchCreate")]
  public virtual async Task<IActionResult> Insert(IEnumerable<TInputModel> inputModels)
  {
    var response = await SanitizerService.InsertAsync(inputModels);
    return StatusCode(response.StatusCode, response);
  }

  [HttpPatch("{id}")]
  public virtual async Task<IActionResult> Update(int? id, TUpdateModel inputModel)
  {
    var response = await SanitizerService.UpdateAsync(id, inputModel);
    return StatusCode(response.StatusCode, response);
  }

  [HttpPatch]
  public virtual async Task<IActionResult> Update(IEnumerable<TUpdateModel> inputModels)
  {
    var response = await SanitizerService.UpdateAsync(inputModels);
    return StatusCode(response.StatusCode, response);
  }

  [HttpDelete("{id}")]
  public virtual async Task<IActionResult> Remove(int? id)
  {
    var response = await SanitizerService.RemoveAsync(id);
    return StatusCode(response.StatusCode, response);
  }

  [HttpDelete]
  public virtual async Task<IActionResult> Remove([FromQuery] IEnumerable<int> ids)
  {
    var response = await SanitizerService.RemoveAsync(ids);
    return StatusCode(response.StatusCode, response);
  }
}