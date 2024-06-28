using Core.Entities;
using Core.V1;

public class FavoriteSearchParams() : SearchParams<Favorite>()
{
  public string? UserId { get; set; }

  public override void ApplyCustomPredicates() => CustomPredicates = f => f.UserId == UserId;
}