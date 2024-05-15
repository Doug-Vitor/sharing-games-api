using Core.Interfaces;

namespace Core.V1.DTOs;

public class RequestUpdateModel : RequestInputModel, IKeyed
{
  public int? Id { get; set; }
}