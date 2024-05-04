using FluentValidation;

internal static class FluentValidatorExtensions
{
  static internal IRuleBuilderOptions<T, TProperty> StopIfNullOrEmpty<T, TProperty>(this IRuleBuilderInitial<T, TProperty> builder)
    => builder.Cascade(CascadeMode.Stop).NotNull().NotEmpty();

  static internal IRuleBuilderOptions<T, TProperty> StopIfNullOrEmpty<T, TProperty>(this IRuleBuilderInitialCollection<T, TProperty> builder)
    => builder.Cascade(CascadeMode.Stop).NotNull().NotEmpty();
}