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
  public IDictionary<string, string[]>? Errors { get; private set; }

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

  public async Task ValidateAsync(TInputModel inputModel) => await ValidateAsync(_singleValidator, inputModel);
  public async Task ValidateAsync(IEnumerable<TInputModel> inputModels) => await ValidateAsync(_enumerableValidator, inputModels);
  public async Task ValidateAsync(TUpdateModel inputModel) => await ValidateAsync(_singleUpdaterValidator, inputModel);
  public async Task ValidateAsync(ValidationContext<TUpdateModel> context) => await ValidateAsync(_singleUpdaterValidator, context);
  public async Task ValidateAsync(IEnumerable<TUpdateModel> inputModels) => await ValidateAsync(_enumerableUpdaterValidator, inputModels);
  public async Task ValidateAsync(ValidationContext<IEnumerable<TUpdateModel>> context) => await ValidateAsync(_enumerableUpdaterValidator, context);

  bool ShouldValidate() => Errors is null;

  async Task ValidateAsync<T>(IValidator<T> validator, T validatable)
  {
    if (ShouldValidate()) SetValidationResults(await validator.ValidateAsync(validatable));
  }

  async Task ValidateAsync<T>(IValidator<T> validator, ValidationContext<T> context)
  {
    if (ShouldValidate()) SetValidationResults(await validator.ValidateAsync(context));
  }

  void SetValidationResults(ValidationResult result)
  {
    IsValid = result.IsValid;
    Errors = result.ToDictionary();
  }
}