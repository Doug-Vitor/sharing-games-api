using Core.Entities.Request;

namespace Core.V1.DTOs;

public class RequestViewModel() : ViewModel(), IOwnedByUser
{
  public string? Title { get; set; }
  public string? Description { get; set; }
  public string UserId { get; set; }

  public static implicit operator RequestViewModel(Suggestion suggestion) => new()
  {
    Id = suggestion.Id,
    Title = suggestion.Title,
    Description = suggestion.Description,
    UserId = suggestion.UserId,
  };
}