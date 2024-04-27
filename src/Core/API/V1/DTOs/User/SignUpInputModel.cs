namespace Core.V1.DTOs;

public class SignUpInputModel()
{
  public string? UserName { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }
  public string? PasswordConfirmation { get; set; }
}