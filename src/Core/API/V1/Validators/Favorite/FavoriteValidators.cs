using Core.Interfaces;
using Core.V1.DTOs;
using FluentValidation;

namespace Core.V1.Validators.Favorite;

public abstract class SingleValidator<T> : AbstractValidator<T>, IValidator<T>, IAppValidator
  where T : FavoriteInputModel
{
  public SingleValidator()
  {
    RuleFor(f => f.UserId).StopIfNullOrEmpty();
    RuleFor(f => f.GameId).StopIfNullOrEmpty();
  }
}

public abstract class ListValidator<T> : AbstractValidator<IEnumerable<T>>, IValidator<IEnumerable<T>>, IAppValidator
  where T : FavoriteInputModel
{
  public ListValidator()
  {
    RuleForEach(f => f.Select(f => f.UserId)).StopIfNullOrEmpty()
                                             .WithName(nameof(FavoriteInputModel.UserId));
    RuleForEach(f => f.Select(f => f.GameId)).StopIfNullOrEmpty()
                                             .WithName(nameof(FavoriteInputModel.GameId));
  }
}

public class FavoriteInputValidator() : SingleValidator<FavoriteInputModel>();
public class FavoriteListInputValidator() : ListValidator<FavoriteInputModel>();
public class FavoriteUpdateValidator() : SingleValidator<FavoriteUpdateModel>();
public class FavoriteListUpdateValidator() : ListValidator<FavoriteUpdateModel>();