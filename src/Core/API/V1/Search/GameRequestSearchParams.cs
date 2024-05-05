using Core.Entities.Request;
using Core.Enums;

namespace Core.V1;

public class GameRequestSearchParams() : SearchParams<GameRequest>()
{
  public string? UserId { get; set; }
  public string? AuthenticatedUserId { get; set; }

  public override void ApplyCustomPredicates()
    => CustomPredicates = request => UserId == null ? request.UserId == AuthenticatedUserId : request.Answer.Status == GameRequestAnswerStatus.Accepted;
}