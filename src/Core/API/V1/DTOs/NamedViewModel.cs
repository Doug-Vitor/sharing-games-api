using Core.Entities;

namespace Core.V1.DTOs;

public class NamedViewModel() : ViewModel()
{
  public string? Name { get; set; }

  public static implicit operator NamedViewModel(NamedEntity entity) => new()
  {
    Id = entity.Id,
    Name = entity.Name
  };
}