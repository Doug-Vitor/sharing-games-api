namespace Core.Entities;

public record Image() : BaseEntity()
{
  public bool IsCoverPhoto { get; init; }
  public string Url { get; init; }
  public string FileName { get; init; }
  public int GameId { get; init; }

  public virtual Game Game { get; init; }
}