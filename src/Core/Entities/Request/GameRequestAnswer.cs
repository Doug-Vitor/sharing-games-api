using Core.Enums;

namespace Core.Entities.Request;

public record GameRequestAnswer() : BaseEntity()
{
  public GameRequestAnswerStatus Status { get; init; } = GameRequestAnswerStatus.Pending;
  public string Message { get; init; } = string.Empty;
  public int GameRequestId { get; init; }

  public virtual GameRequest GameRequest { get; init; }
}