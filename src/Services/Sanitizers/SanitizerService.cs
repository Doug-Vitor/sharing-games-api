using System.Net;
using Core.Entities;
using Core.Interfaces;
using Core.Response;
using Core.V1;

namespace Services;

public class SanitizerService<T, TViewModel, TInputModel, TUpdateModel>
  : ISanitizerService<T, TViewModel, TInputModel, TUpdateModel>
  where T : BaseEntity where TViewModel : class where TInputModel : class where TUpdateModel : class, IKeyed
{
  public IReadonlySanitizerService<T, TViewModel> ReadonlySanitizerService { get; init; }
  public IWritableSanitizerService<T, TViewModel, TInputModel, TUpdateModel> WritableSanitizerService { get; init; }
  public IValidatorService<TInputModel, TUpdateModel> Validator { get; init; }

  public SanitizerService(
    IReadonlySanitizerService<T, TViewModel> readonlySanitizerService,
    IWritableSanitizerService<T, TViewModel, TInputModel, TUpdateModel> writableSanitizerService,
    IValidatorService<TInputModel, TUpdateModel> validator
  )
  {
    ReadonlySanitizerService = readonlySanitizerService;
    WritableSanitizerService = writableSanitizerService;
    Validator = validator;
  }

  public virtual async Task<ActionResponse> InsertAsync(TInputModel inputModel)
  {
    await Validator.ValidateAsync(inputModel);
    if (Validator.IsValid) return await WritableSanitizerService.InsertAsync(inputModel);
    return new ErrorResponse((int)HttpStatusCode.UnprocessableEntity, Validator.Errors);
  }

  public virtual async Task<ActionResponse> InsertAsync(IEnumerable<TInputModel> inputModels)
  {
    await Validator.ValidateAsync(inputModels);
    if (Validator.IsValid) return await WritableSanitizerService.InsertAsync(inputModels);

    return new ErrorResponse((int)HttpStatusCode.UnprocessableEntity, Validator.Errors);
  }

  public virtual async Task<ActionResponse> GetByIdAsync(int? id, IEnumerable<string>? propertyNamesToBeIncluded)
    => await ReadonlySanitizerService.GetByIdAsync(id, propertyNamesToBeIncluded);

  public virtual async Task<ActionResponse> GetAllAsync(SearchParams<T>? searchParams, IEnumerable<string>? propertyNamesToBeIncluded)
    => await ReadonlySanitizerService.GetAllAsync(searchParams, propertyNamesToBeIncluded);

  public virtual async Task<ActionResponse> UpdateAsync(int? id, TUpdateModel inputModel)
  {
    await Validator.ValidateAsync(inputModel);
    if (Validator.IsValid) return await WritableSanitizerService.UpdateAsync(id, inputModel);
    return new ErrorResponse((int)HttpStatusCode.UnprocessableEntity, Validator.Errors);
  }

  public virtual async Task<ActionResponse> UpdateAsync(IEnumerable<TUpdateModel> inputModels)
  {
    await Validator.ValidateAsync(inputModels);
    if (Validator.IsValid) return await WritableSanitizerService.UpdateAsync(inputModels);
    return new ErrorResponse((int)HttpStatusCode.UnprocessableEntity, Validator.Errors);
  }

  public virtual async Task<ActionResponse> RemoveAsync(int? id) => await WritableSanitizerService.RemoveAsync(id);
  public virtual async Task<ActionResponse> RemoveAsync(IEnumerable<int>? ids) => await WritableSanitizerService.RemoveAsync(ids);
}