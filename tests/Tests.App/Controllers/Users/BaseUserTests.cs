namespace Tests.App.Controllers;

public abstract class BaseUserTests : AuthenticatedBaseTest
{
  public BaseUserTests(string? baseAddress = "/api/Users") : base(baseAddress) { }
}