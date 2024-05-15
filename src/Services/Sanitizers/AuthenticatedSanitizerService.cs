using System.Net;
using Core.Entities;
using Core.Interfaces;
using Core.Response;
using Core.V1.DTOs;
using Services.Interfaces;

namespace Services;

delegate Task<ActionResponse> OnOwnershipValidated();

public class AuthenticatedSanitizerService<T, TViewModel, TInputModel, TUpdateModel>(
  IReadonlySanitizerService<T, TViewModel> readonlySanitizerService,
  IWritableSanitizerService<T, TViewModel, TInputModel, TUpdateModel> writableSanitizerService,
  IValidatorService<TInputModel, TUpdateModel> validator,
  IAuthenticationService authService
) : SanitizerService<T, TViewModel, TInputModel, TUpdateModel>(
  readonlySanitizerService,
  writableSanitizerService,
  validator
), IAuthenticatedSanitizerService<T, TViewModel, TInputModel, TUpdateModel> where T : BaseEntity, IOwnedByUser where TViewModel : ViewModel, IOwnedByUser where TInputModel : class, IOwnedByUser where TUpdateModel : class, IKeyed, IOwnedByUser
{
  protected readonly string? AuthenticatedUserId = authService.AuthenticatedUserId;

  public override Task<ActionResponse> InsertAsync(TInputModel inputModel)
  {
    inputModel.UserId = AuthenticatedUserId;
    return base.InsertAsync(inputModel);
  }

  public override Task<ActionResponse> InsertAsync(IEnumerable<TInputModel> inputModels)
  {
    foreach (var inputModel in inputModels)
      inputModel.UserId = AuthenticatedUserId;

    return base.InsertAsync(inputModels);
  }

  public override async Task<ActionResponse> UpdateAsync(int? id, TUpdateModel inputModel) =>
    await ValidateOwnership(id, async () =>
    {
      inputModel.UserId = AuthenticatedUserId;
      return await base.UpdateAsync(id, inputModel);
    });

  public override async Task<ActionResponse> UpdateAsync(IEnumerable<TUpdateModel> inputModels) =>
    await ValidateOwnership(inputModels.Select(input => input.Id.GetValueOrDefault()), async () =>
    {
      foreach (var inputModel in inputModels)
        inputModel.UserId = AuthenticatedUserId;

      return await base.UpdateAsync(inputModels);
    });

  public override async Task<ActionResponse> RemoveAsync(int? id)
    => await ValidateOwnership(id, async () => await WritableSanitizerService.RemoveAsync(id));


  public override async Task<ActionResponse> RemoveAsync(IEnumerable<int>? ids)
    => await ValidateOwnership(ids, async () => await base.RemoveAsync(ids));

  async Task<ActionResponse> ValidateOwnership(int? id, OnOwnershipValidated onOwnershipValidated)
  {
    var response = await ReadonlySanitizerService.GetByIdAsync(id);

    if (response is SuccessResponse<TViewModel> successResponse && successResponse.Data.UserId == AuthenticatedUserId)
      return await onOwnershipValidated();

    return response is ErrorResponse ? response : new ActionResponse((int)HttpStatusCode.Forbidden);
  }

  async Task<ActionResponse> ValidateOwnership(IEnumerable<int> ids, OnOwnershipValidated onOwnershipValidated)
  {
    var response = await ReadonlySanitizerService.GetAllAsync(new()
    {
      CustomPredicates = entity => ids.Contains(entity.Id),
      PerPage = ids.Count()
    });

    var ownershipValidation = response is SuccessResponse<IEnumerable<TViewModel>> successResponse
                              && successResponse.Data.All(data => data.UserId == AuthenticatedUserId);

    if (ownershipValidation) return await onOwnershipValidated();
    return response is ErrorResponse ? response : new ActionResponse((int)HttpStatusCode.Forbidden);
  }
}