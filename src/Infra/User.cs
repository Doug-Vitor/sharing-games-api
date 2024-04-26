using Core.Entities.Request;
using Core.V1.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Infra.Entities;

public class User() : IdentityUser
{
  public virtual ICollection<Suggestion> Suggestions { get; init; }
  public virtual ICollection<GameRequest> GameRequests { get; init; }

  public static implicit operator User(SignUpInputModel user) => new()
  {
    UserName = user.UserName,
    Email = user.Email
  };
}