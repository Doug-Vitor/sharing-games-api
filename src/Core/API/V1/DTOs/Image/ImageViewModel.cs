using Core.Entities;

namespace Core.V1.DTOs;

public class ImageViewModel()
{
  public string? Url { get; set; }

  public static implicit operator ImageViewModel(Image image) => new() { Url = image.Url };
}