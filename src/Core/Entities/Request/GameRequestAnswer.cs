using Core.Enums;

namespace Core.Entities.Request;

public record GameRequestAnswer() : BaseEntity()
{
  public GameRequestAnswerStatus Status { get; init; }
  public string Message { get; init; }
  public int GameRequestId { get; init; }

  public virtual GameRequest GameRequest { get; init; }
}