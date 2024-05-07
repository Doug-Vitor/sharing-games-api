using Core.Enums;
using Core.V1.DTOs;
using Core.V1.Validators.GameRequest;
using FluentValidation;

public class GameRequestUpdateValidator : SingleValidator<GameRequestUpdateModel>
{
  public GameRequestUpdateValidator() : base()
  {
    RuleFor(r => r.GameRequestAnswerId).Custom((_, context) =>
    {
      context.RootContextData.TryGetValue(Constants.GameRequestContextSharedKey, out object? status);
      if (Enum.Parse<GameRequestAnswerStatus>(status as string) != GameRequestAnswerStatus.Pending)
        context.AddFailure("Unable to update a request that was already answered.");
    });
  }
}

public class GameRequestListUpdateValidator : ListValidator<GameRequestUpdateModel>
{
  public GameRequestListUpdateValidator() : base()
  {
    RuleForEach(r => r.Select(r => r.GameRequestAnswerId)).Custom((_, context) =>
    {
      context.RootContextData.TryGetValue(Constants.GameRequestContextSharedKey, out object? status);
      if (Enum.Parse<GameRequestAnswerStatus>(status as string) != GameRequestAnswerStatus.Pending)
        context.AddFailure("Unable to update a request that was already answered.");
    });
  }
}