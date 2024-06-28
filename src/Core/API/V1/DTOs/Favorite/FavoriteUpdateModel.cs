using Core.Interfaces;

namespace Core.V1.DTOs;

public class FavoriteUpdateModel : FavoriteInputModel, IKeyed
{
  public int? Id { get; set; }
}
