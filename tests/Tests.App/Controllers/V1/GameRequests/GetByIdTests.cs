using System.Net;
using Core.Entities.Request;
using Core.Enums;
using Core.Response;
using Core.V1.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Tests.App.Controllers.V1.GameRequests;

public class GetByIdTests() : BaseTest()
{
  [Fact]
  public async Task WhenUnauthenticatedShouldReturnUnauthorized()
  {
    var response = await Client.GetAsync(BaseAddress);
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
  public async Task WhenAcceptedGameRequestIdOfAnotherUserProvidedShouldReturnTheCorrespondingGameRequest()
  {
    await SetupWithAutentication();
    var request = DbSet.Include(game => game.Answer).First(
      g => g.UserId != AuthenticatedUser.Id && g.Answer.Status == GameRequestAnswerStatus.Accepted
    );

    var response = await GetSingleAndValidateAsync($"/{request.Id}");
    ApplySharedAsserts(request, response);
  }

  [Fact]
  public async Task WhenValidIdProvidedShouldReturnTheCorrespondingGameRequest()
  {
    await SetupWithAutentication();
    var request = DbSet.Include(request => request.Answer).First();

    var response = await GetSingleAndValidateAsync($"/{request.Id}");
    ApplySharedAsserts(request, response);
  }

  static void ApplySharedAsserts(GameRequest request, SuccessResponse<GameRequestViewModel> response)
  {
    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.Equal(request.Id, response.Data.Id);
    Assert.Equal(request.Title, response.Data.Title);
    Assert.Equal(request.Description, response.Data.Description);
    Assert.Equal(request.Answer.Status.ToString(), response.Data.AnswerStatus);
    Assert.Equal(request.UserId, response.Data.UserId);
  }
}