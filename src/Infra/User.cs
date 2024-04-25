using Core.Entities.Request;
using Microsoft.AspNetCore.Identity;

namespace Infra.Entities;

public class User() : IdentityUser
{
  public virtual ICollection<Suggestion> Suggestions { get; init; }
  public virtual ICollection<GameRequest> GameRequests { get; init; }
}