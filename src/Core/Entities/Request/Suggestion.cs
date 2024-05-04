using Core.V1.DTOs;

namespace Core.Entities.Request;

public record Suggestion() : BaseRequest()
{
  public static implicit operator Suggestion(RequestInputModel inputModel) => new()
  {
    Title = inputModel.Title,
    Description = inputModel.Description,
    UserId = inputModel.UserId
  };
}