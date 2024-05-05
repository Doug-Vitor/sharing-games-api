using Core.V1.DTOs;
using FluentValidation;

namespace Core.V1.Validators.GameRequest;

public abstract class SingleValidator<T> : Validators.SingleValidator<T>
  where T : GameRequestInputModel
{
  public SingleValidator() : base() => RuleFor(r => r.GameUrl).StopIfNullOrEmpty();
}

public abstract class ListValidator<T> : RangeValidator<T>
  where T : GameRequestInputModel
{
  public ListValidator() : base()
    => RuleForEach(r => r.Select(r => r.GameUrl)).StopIfNullOrEmpty()
                                                 .WithName(nameof(GameRequestInputModel.GameUrl));
}