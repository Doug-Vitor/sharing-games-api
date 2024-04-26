namespace Core.V1.DTOs;

public class UserViewModel()
{
  public string UserName { get; init; }

  public UserViewModel(string userName) : this() => UserName = userName;

  public static implicit operator UserViewModel(SignInInputModel user) => new(user.UserName);
  public static implicit operator UserViewModel(SignUpInputModel user) => new(user.UserName);
}