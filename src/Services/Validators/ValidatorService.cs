using Core.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Services.Validators;

public class ValidatorService<TInputModel, TUpdateModel> : IValidatorService<TInputModel, TUpdateModel>
  where TInputModel : class where TUpdateModel : class
{
  readonly IValidator<TInputModel> _singleValidator;
  readonly IValidator<IEnumerable<TInputModel>> _enumerableValidator;
  readonly IValidator<TUpdateModel> _singleUpdaterValidator;
  readonly IValidator<IEnumerable<TUpdateModel>> _enumerableUpdaterValidator;

  public bool IsValid { get; private set; }
  public IDictionary<string, string[]> Errors { get; private set; }

  public ValidatorService(
    IValidator<TInputModel> singleValidator,
    IValidator<IEnumerable<TInputModel>> enumerableValidator,
    IValidator<TUpdateModel> singleUpdaterValidator,
    IValidator<IEnumerable<TUpdateModel>> enumerableUpdaterValidator
  )
  {
    _singleValidator = singleValidator;
    _enumerableValidator = enumerableValidator;
    _singleUpdaterValidator = singleUpdaterValidator;
    _enumerableUpdaterValidator = enumerableUpdaterValidator;
  }

  public async Task ValidateAsync(TInputModel inputModel)
    => SetValidationResults(await _singleValidator.ValidateAsync(inputModel));

  public async Task ValidateAsync(IEnumerable<TInputModel> inputModels)
    => SetValidationResults(await _enumerableValidator.ValidateAsync(inputModels));

  public async Task ValidateAsync(TUpdateModel inputModel)
    => SetValidationResults(await _singleUpdaterValidator.ValidateAsync(inputModel));

  public async Task ValidateAsync(IEnumerable<TUpdateModel> inputModels)
    => SetValidationResults(await _enumerableUpdaterValidator.ValidateAsync(inputModels));

  void SetValidationResults(ValidationResult result)
  {
    IsValid = result.IsValid;
    Errors = result.ToDictionary();
  }
}