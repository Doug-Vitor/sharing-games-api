using Core.Entities;

namespace Core.V1.DTOs;

public class ViewModel()
{
  public int? Id { get; set; }

  public static implicit operator ViewModel(BaseEntity entity) => new() { Id = entity.Id };
}