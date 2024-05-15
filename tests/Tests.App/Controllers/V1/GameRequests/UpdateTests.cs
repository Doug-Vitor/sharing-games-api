using System.Net;
using Core.Entities.Request;
using Core.Enums;
using Core.Response;
using Core.V1.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Tests.App.Controllers.V1.GameRequests;

public class UpdateTests() : BaseTest()
{
  [Fact]
  public async Task WhenUnauthenticatedShouldReturnUnauthorized()
  {
    var response = await Client.PatchAsync(BaseAddress, null);
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Fact]
  public async Task WhenInvalidIdProvidedShouldReturnNotFound()
  {
    await SetupWithAutentication();
    var response = await GetAndParseAsync<ErrorResponse>("/0");
    Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
  }

  [Fact]
  public async Task WhenInvalidBodyProvidedShouldReturnUnprocessableEntity()
  {
    await SetupWithAutentication();
    GameRequest existingGameRequest = await DbSet.FirstAsync(g => g.UserId == AuthenticatedUser.Id);
    GameRequestUpdateModel inputModel = new()
    {
      Id = existingGameRequest.Id,
      Title = "",
      Description = "",
      GameUrl = "",
    };

    var response = await PatchAndParseAsync<ErrorResponse>(inputModel, $"/{existingGameRequest.Id}");

    Assert.Equal((int)HttpStatusCode.UnprocessableEntity, response.StatusCode);
    Assert.Contains(nameof(GameRequestInputModel.Title), response.Messages);
    Assert.Contains(nameof(GameRequestInputModel.Description), response.Messages);
    Assert.Contains(nameof(GameRequestInputModel.GameUrl), response.Messages);
    Assert.NotEmpty(response.Messages.Values.SelectMany(errorDescription => errorDescription));
  }

  [Fact]
  public async Task WhenGameRequestIdOfAnotherUserProvidedShouldReturnForbidden()
  {
    await SetupWithAutentication();

    GameRequest existingGameRequest = await DbSet.FirstAsync(g => g.UserId != AuthenticatedUser.Id);
    var response = await PatchAndParseAsync<ErrorResponse>(
      new GameRequestUpdateModel() { Id = existingGameRequest.Id },
      $"/{existingGameRequest.Id}"
    );

    Assert.Equal((int)HttpStatusCode.Forbidden, response.StatusCode);
  }

  [Fact]
  public async Task WhenGameRequestAlreadyAnsweredShouldReturnUnprocessableEntity()
  {
    await SetupWithAutentication();
    GameRequest existingGameRequest = await DbSet.Include(g => g.Answer).FirstAsync(
      g => g.UserId == AuthenticatedUser.Id && g.Answer.Status != GameRequestAnswerStatus.Pending
    );

    var response = await PatchAndParseAsync<ErrorResponse>(
      new GameRequestUpdateModel() { Id = existingGameRequest.Id },
      $"/{existingGameRequest.Id}"
    );

    Assert.Equal((int)HttpStatusCode.UnprocessableEntity, response.StatusCode);
    Assert.Contains(nameof(GameRequestUpdateModel.GameRequestAnswerId), response.Messages);
    Assert.NotEmpty(response.Messages.Values.SelectMany(errorDescription => errorDescription));
  }

  [Fact]
  public async Task WhenValidBodyProvidedShouldUpdateTheGameRequest()
  {
    await SetupWithAutentication();
    GameRequest existingGameRequest = await DbSet.Include(g => g.Answer).FirstAsync(
      g => g.UserId == AuthenticatedUser.Id && g.Answer.Status == GameRequestAnswerStatus.Pending
    );

    GameRequestUpdateModel inputModel = new()
    {
      Id = existingGameRequest.Id,
      Title = existingGameRequest.Title,
      GameUrl = existingGameRequest.GameUrl,
      Description = "Please add this for me!!!",
      UserId = existingGameRequest.UserId,
    };

    var response = await UpdateAndValidateAsync($"/{existingGameRequest.Id}", inputModel);
    Assert.Equal(inputModel.Id, response.Data.Id);
    Assert.Equal(inputModel.Title, response.Data.Title);
    Assert.Equal(inputModel.Description, response.Data.Description);
  }
}