using Infra.Entities;

namespace Core.V1.DTOs;

public class UserViewModel()
{
  public string Id { get; init; }
  public string UserName { get; init; }

  public UserViewModel(string userName) : this() => UserName = userName;
  public UserViewModel(string id, string userName) : this(userName) => Id = id;

  public static implicit operator UserViewModel(User user) => new(user.Id, user.UserName);
  public static implicit operator UserViewModel(SignInInputModel user) => new(user.UserName);
  public static implicit operator UserViewModel(SignUpInputModel user) => new(user.UserName);
}