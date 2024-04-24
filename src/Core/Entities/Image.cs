namespace Core.Entities;

public record Image() : BaseEntity()
{
  public readonly bool IsCoverPhoto;
  public readonly string Url;
  public readonly string FileName;
  public readonly int GameId;

  public virtual Game Game { get; init; }
}