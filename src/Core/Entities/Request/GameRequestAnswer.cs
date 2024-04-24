using Core.Enums;

namespace Core.Entities.Request;

public record GameRequestAnswer() : BaseEntity()
{
  public readonly GameRequestAnswerStatus Status;
  public readonly string Message;
}