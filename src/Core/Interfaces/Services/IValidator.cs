using FluentValidation;

namespace Core.Interfaces;

public interface IValidatorService<TInputModel, TUpdateModel>
  where TInputModel : class where TUpdateModel : class
{
  bool IsValid { get; }
  IDictionary<string, string[]>? Errors { get; }
  Task ValidateAsync(TInputModel inputModel);
  Task ValidateAsync(IEnumerable<TInputModel> inputModels);
  Task ValidateAsync(TUpdateModel inputModel);
  Task ValidateAsync(ValidationContext<TUpdateModel> context);
  Task ValidateAsync(IEnumerable<TUpdateModel> inputModels);
  Task ValidateAsync(ValidationContext<IEnumerable<TUpdateModel>> context);
}