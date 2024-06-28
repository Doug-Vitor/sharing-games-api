using Core.Entities;
using Core.Interfaces;
using Core.Response;
using Core.V1.DTOs;
using Services.Interfaces;

namespace Services.Sanitizers;

public class FavoriteSanitizerService(
  IReadonlySanitizerService<Favorite, FavoriteViewModel> readonlySanitizerService,
  IWritableSanitizerService<Favorite, FavoriteViewModel, FavoriteInputModel, FavoriteUpdateModel> writableSanitizerService,
  IValidatorService<FavoriteInputModel, FavoriteUpdateModel> validator,
  IAuthenticationService authService
) : AuthenticatedSanitizerService<Favorite, FavoriteViewModel, FavoriteInputModel, FavoriteUpdateModel>(
  readonlySanitizerService,
  writableSanitizerService,
  validator,
  authService
)
{
  public new async Task<ActionResponse> GetAllAsync(FavoriteSearchParams searchParams, IEnumerable<string>? propertyNamesToBeIncluded)
  {
    searchParams.UserId = AuthenticatedUserId;
    return await base.GetAllAsync(searchParams, propertyNamesToBeIncluded);
  }
}