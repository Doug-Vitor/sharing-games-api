using Core.Interfaces;
using Core.V1.DTOs;
using FluentValidation;

namespace Core.V1.Validators;

public abstract class SingleValidator<T> : AbstractValidator<T>, IValidator<T>, IAppValidator
  where T : RequestInputModel
{
  public SingleValidator()
  {
    RuleFor(r => r.Title).StopIfNullOrEmpty().MaximumLength(Constants.DefaultMaxLengthOfString);
    RuleFor(r => r.Description).StopIfNullOrEmpty();
    RuleFor(r => r.UserId).StopIfNullOrEmpty();
  }
}

public abstract class RangeValidator<T> : AbstractValidator<IEnumerable<T>>, IValidator<IEnumerable<T>>, IAppValidator
  where T : RequestInputModel
{
  public RangeValidator()
  {
    RuleForEach(r => r.Select(r => r.Title)).StopIfNullOrEmpty()
                                            .MaximumLength(Constants.DefaultMaxLengthOfString)
                                            .WithName(nameof(RequestInputModel.Title));

    RuleForEach(r => r.Select(r => r.Description)).StopIfNullOrEmpty()
                                                  .WithName(nameof(RequestInputModel.Description));

    RuleForEach(r => r.Select(r => r.UserId)).StopIfNullOrEmpty()
                                             .WithName(nameof(RequestInputModel.UserId));
  }
}

public class RequestInputValidator() : SingleValidator<RequestInputModel>() { }
public class RequestUpdateValidator() : SingleValidator<RequestUpdateModel>() { }
public class RangeRequestInputValidator() : RangeValidator<RequestInputModel>() { }
public class RangeRequestUpdateValidator() : RangeValidator<RequestUpdateModel>() { }