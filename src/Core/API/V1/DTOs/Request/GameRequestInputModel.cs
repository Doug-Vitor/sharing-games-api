using Core.Entities.Request;

namespace Core.V1.DTOs;

public class GameRequestInputModel() : RequestInputModel()
{
  public string? GameUrl { get; set; }
}