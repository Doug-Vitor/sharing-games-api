using Core.Interfaces;

namespace Core.V1.DTOs;

public class GameRequestUpdateModel : GameRequestInputModel, IKeyed
{
  public int? Id { get; set; }
  public int? GameRequestAnswerId { get; set; }
}