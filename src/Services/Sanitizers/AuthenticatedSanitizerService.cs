using Core.Entities;
using Core.Interfaces;
using Core.Response;
using Core.V1.DTOs;
using Services.Interfaces;

namespace Services;

public class AuthenticatedSanitizerService<T, TViewModel, TInputModel, TUpdateModel>(
  IReadonlySanitizerService<T, TViewModel> readonlySanitizerService,
  IWritableSanitizerService<T, TViewModel, TInputModel, TUpdateModel> writableSanitizerService,
  IValidatorService<TInputModel, TUpdateModel> validator,
  IAuthenticationService authService
) : SanitizerService<T, TViewModel, TInputModel, TUpdateModel>(
  readonlySanitizerService,
  writableSanitizerService,
  validator
), IAuthenticatedSanitizerService<T, TViewModel, TInputModel, TUpdateModel> where T : BaseEntity, IOwnedByUser where TViewModel : ViewModel where TInputModel : class, IOwnedByUser where TUpdateModel : class, IOwnedByUser
{
  readonly string? _authenticatedUserId = authService.AuthenticatedUserId;

  public override Task<ActionResponse> InsertAsync(TInputModel inputModel)
  {
    inputModel.UserId = _authenticatedUserId;
    return base.InsertAsync(inputModel);
  }

  public override Task<ActionResponse> InsertAsync(IEnumerable<TInputModel> inputModels)
  {
    foreach (var inputModel in inputModels)
      inputModel.UserId = _authenticatedUserId;

    return base.InsertAsync(inputModels);
  }

  public override Task<ActionResponse> UpdateAsync(int? id, TUpdateModel inputModel)
  {
    inputModel.UserId = _authenticatedUserId;
    return base.UpdateAsync(id, inputModel);
  }

  public override Task<ActionResponse> UpdateAsync(IEnumerable<TUpdateModel> inputModels)
  {
    foreach (var inputModel in inputModels)
      inputModel.UserId = _authenticatedUserId;

    return base.UpdateAsync(inputModels);
  }
}