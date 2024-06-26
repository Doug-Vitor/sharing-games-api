using Core.Entities.Request;
using Core.Interfaces;
using Core.Response;
using Core.V1;
using Core.V1.DTOs;
using FluentValidation;
using Services.Interfaces;

namespace Services.Sanitizers;

delegate ActionResponse OnSuccessHandler(ActionResponse response);

public class GameRequestSanitizerService(
  IReadonlySanitizerService<GameRequest, GameRequestViewModel> readonlySanitizerService,
  IReadonlyRepository<GameRequestAnswer> requestAnswerReadonlyRepository,
  IWritableRepository<GameRequestAnswer> requestAnswerWritableRepository,
  IWritableSanitizerService<GameRequest, GameRequestViewModel, GameRequestInputModel, GameRequestUpdateModel> writableSanitizerService,
  IValidatorService<GameRequestInputModel, GameRequestUpdateModel> validator,
  IAuthenticationService authService
) : AuthenticatedSanitizerService<GameRequest, GameRequestViewModel, GameRequestInputModel, GameRequestUpdateModel>(
  readonlySanitizerService,
  writableSanitizerService,
  validator,
  authService
)
{
  readonly IReadonlyRepository<GameRequestAnswer> _requestAnswerReadonlyRepository = requestAnswerReadonlyRepository;
  readonly IWritableRepository<GameRequestAnswer> _requestAnswerWritableRepository = requestAnswerWritableRepository;

  public override async Task<ActionResponse> InsertAsync(GameRequestInputModel inputModel)
  {
    var result = await base.InsertAsync(inputModel);

    if (result is SuccessResponse<GameRequestViewModel> response)
      await CreateAnswers(new List<int>() { response.Data.Id.Value });

    return result;
  }

  public override async Task<ActionResponse> InsertAsync(IEnumerable<GameRequestInputModel> inputModels)
  {
    var result = await base.InsertAsync(inputModels);

    if (result is SuccessResponse<IEnumerable<GameRequestViewModel>> response)
      await CreateAnswers(response.Data.Select(request => request.Id.Value));

    return result;
  }

  public new async Task<ActionResponse> GetAllAsync(GameRequestSearchParams searchParams, IEnumerable<string>? propertyNamesToBeIncluded)
  {
    searchParams.AuthenticatedUserId = AuthenticatedUserId;
    return await base.GetAllAsync(searchParams, propertyNamesToBeIncluded);
  }

  public override async Task<ActionResponse> UpdateAsync(int? id, GameRequestUpdateModel inputModel)
  {
    ValidationContext<GameRequestUpdateModel> context = await GetValidationContext(inputModel);
    await Validator.ValidateAsync(context);
    return await base.UpdateAsync(id, inputModel);
  }

  public override async Task<ActionResponse> UpdateAsync(IEnumerable<GameRequestUpdateModel> inputModels)
  {
    ValidationContext<IEnumerable<GameRequestUpdateModel>> context = await GetValidationContext(inputModels);
    await Validator.ValidateAsync(context);
    return await base.UpdateAsync(inputModels);
  }

  async Task<ValidationContext<GameRequestUpdateModel>> GetValidationContext(GameRequestUpdateModel inputModel)
  {
    ValidationContext<GameRequestUpdateModel> context = new(inputModel);
    var gameRequest = await ReadonlySanitizerService.ReadonlyRepository.GetByIdAsync(inputModel.Id, [nameof(GameRequest.Answer)]);
    context.RootContextData.Add(Constants.GameRequestContextSharedKey, gameRequest.Answer.Status);
    return context;
  }

  async Task<ValidationContext<IEnumerable<GameRequestUpdateModel>>> GetValidationContext(IEnumerable<GameRequestUpdateModel> inputModels)
  {
    ValidationContext<IEnumerable<GameRequestUpdateModel>> context = new(inputModels);
    var answers = await GetAnswers(inputModels.Select(i => i.GameRequestAnswerId));
    context.RootContextData.Add(Constants.GameRequestContextSharedKey, answers.Select(answer => answer.Status));

    return context;
  }

  async Task CreateAnswers(IEnumerable<int> requestIds)
  {
    IEnumerable<GameRequestAnswer> answers = requestIds.Select(
      id => new GameRequestAnswer() { GameRequestId = id }
    );

    await _requestAnswerWritableRepository.InsertAsync(answers);
  }

  async Task<IEnumerable<GameRequestAnswer>> GetAnswers(IEnumerable<int?> ids)
  {
    SearchParams<GameRequestAnswer> searchParams = new()
    {
      CustomPredicates = answer => ids.Contains(answer.GameRequestId),
      PerPage = ids.Count()
    };

    return await _requestAnswerReadonlyRepository.GetAllAsync(searchParams, []);
  }
}