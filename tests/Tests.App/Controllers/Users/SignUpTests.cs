using System.Net;
using Core.Response;
using Core.V1.DTOs;

namespace Tests.App.Controllers;

public class SignUpTests() : BaseUserTests()
{
  [Fact]
  public async Task WhenWeakPasswordProvidedShouldNotSignUp()
  {
    SignUpInputModel signUpModel = new() { Password = "weak" };
    var response = await PostAndParseAsync<ErrorResponse>(signUpModel);
    ApplyDefaultAsserts(
      ["PasswordTooShort", "PasswordRequiresNonAlphanumeric", "PasswordRequiresDigit", "PasswordRequiresUpper"],
      response
    );
  }

  [Fact]
  public async Task WhenNoEmailProvidedShouldNotSignUp()
  {
    SignUpInputModel signUpModel = new() { UserName = "user.name", Password = "Str0ngP4ss!" };
    var response = await PostAndParseAsync<ErrorResponse>(signUpModel);
    Assert.Equal((int)HttpStatusCode.UnprocessableEntity, response.StatusCode);
  }

  [Fact]
  public async Task WhenNoUserNameProvidedShouldNotSignUp()
  {
    SignUpInputModel signUpModel = new() { Password = "Str0ngP4ss!" };
    var response = await PostAndParseAsync<ErrorResponse>(signUpModel);
    ApplyDefaultAsserts(["InvalidUserName"], response);
  }

  [Fact]
  public async Task WhenUserNameAlreadyExistsShouldNotSignUp()
  {
    SignUpInputModel signUpModel = new() { UserName = "user.name", Password = "Str0ngP4ss!" };
    await AuthService.SignUpAsync(signUpModel);

    var response = await PostAndParseAsync<ErrorResponse>(signUpModel);
    ApplyDefaultAsserts(["DuplicateUserName"], response);
  }

  [Fact]
  public async Task WhenValidBodyProvidedShouldSignUp()
  {
    SignUpInputModel signUpModel = new()
    {
      UserName = "MySecondUserName",
      Email = "fake@email.com",
      Password = "Str0ngP4ss!"
    };

    var response = await PostAndParseAsync<SuccessResponse<UserViewModel>>(signUpModel);
    Assert.Equal((int)HttpStatusCode.Created, response.StatusCode);
    Assert.Equal(signUpModel.UserName, response.Data.UserName);
  }

  static void ApplyDefaultAsserts(string[] expectedErrorKeys, ErrorResponse response)
  {
    Assert.Equal((int)HttpStatusCode.UnprocessableEntity, response.StatusCode);
    foreach (var errorKey in expectedErrorKeys)
      Assert.Contains(errorKey, response.Messages.Keys);

    Assert.NotEmpty(response.Messages.Values);
  }
}