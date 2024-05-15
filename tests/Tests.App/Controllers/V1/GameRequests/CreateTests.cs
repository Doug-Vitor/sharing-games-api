using System.Net;
using Core.Enums;
using Core.Response;
using Core.V1.DTOs;

namespace Tests.App.Controllers.V1.GameRequests;

public class CreateTests() : BaseTest()
{
  [Fact]
  public async Task WhenUnauthenticatedShouldReturnUnauthorized()
  {
    var response = await Client.PostAsync(BaseAddress, null);
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Fact]
  public async Task WhenInvalidBodyProvidedShouldReturnUnprocessableEntity()
  {
    await SetupWithAutentication();
    GameRequestInputModel inputModel = new() { Title = "", Description = "" };

    var response = await PostAndParseAsync<ErrorResponse>(inputModel);

    Assert.Equal((int)HttpStatusCode.UnprocessableEntity, response.StatusCode);
    Assert.Contains(nameof(GameRequestInputModel.Title), response.Messages);
    Assert.Contains(nameof(GameRequestInputModel.Description), response.Messages);
    Assert.Contains(nameof(GameRequestInputModel.GameUrl), response.Messages);
    Assert.NotEmpty(response.Messages.Values.SelectMany(errorDescription => errorDescription));
  }

  [Fact]
  public async Task WhenValidBodyProvidedShouldCreateTheGameRequest()
  {
    await SetupWithAutentication();
    GameRequestInputModel inputModel = new()
    {
      Title = "This is an awesome game",
      Description = "Please add this for me",
      GameUrl = "sharing-games.com",
    };

    var response = await PostAndValidateAsync(inputModel);
    Assert.Equal(inputModel.Title, response.Data.Title);
    Assert.Equal(inputModel.Description, response.Data.Description);
    Assert.Equal(GameRequestAnswerStatus.Pending.ToString(), response.Data.AnswerStatus);
    Assert.Equal(AuthenticatedUser.Id, response.Data.UserId);
    Assert.IsType<int>(response.Data.Id);
  }
}