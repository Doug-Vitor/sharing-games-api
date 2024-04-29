using Core.Entities;

namespace Core.V1.DTOs;

public class GameViewModel() : NamedViewModel()
{
  public string? Synopsis { get; set; }
  public string? ImageUrl { get; set; }
  public NamedViewModel? Publisher { get; set; }
  public DateOnly? ReleasedAt { get; set; }
  public IEnumerable<ImageViewModel>? Images { get; set; }
  public IEnumerable<Genre>? Genres { get; set; }

  public static implicit operator GameViewModel(Game game) => new()
  {
    Id = game.Id,
    Name = game.Name,
    Synopsis = game.Synopsis,
    ImageUrl = game.Images.FirstOrDefault(image => image.IsCoverPhoto)?.Url,
    Publisher = game.Publisher,
    ReleasedAt = game.ReleasedAt,
    Images = game.Images.Where(image => image.IsCoverPhoto is false).Select(image => (ImageViewModel)image),
    Genres = game.Genres,
  };
}