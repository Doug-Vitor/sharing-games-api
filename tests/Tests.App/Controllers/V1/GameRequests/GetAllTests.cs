using System.Net;
using Core.Enums;
using Infra.Entities;

namespace Tests.App.Controllers.V1.GameRequests;

public class GetAllTests() : BaseTest()
{
  [Fact]
  public async Task WhenUnauthenticatedShouldReturnUnauthorized()
  {
    var response = await Client.GetAsync(BaseAddress);
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Fact]
  public async Task WhenNoSearchParamsProvidedShouldReturnOnlyRequestsOffTheAuthenticatedUser()
  {
    await SetupWithAutentication();
    var requests = await GetManyAndValidateAsync();

    foreach (var request in requests.Data)
      Assert.Equal(AuthenticatedUser.Id, request.UserId);
  }

  [Fact]
  public async Task WhenCursorProvidedShouldPaginateTheResponse()
  {
    await SetupWithAutentication();
    int cursor = DbSet.OrderBy(g => g.Id).First().Id + 1;

    var response = await GetManyAndValidateAsync($"?cursor={cursor}");
    Assert.True(response.Data.All(game => game.Id > cursor));
  }

  [Fact]
  public async Task WhenUserIdProvidedShouldReturnOnlyAcceptedRequestsOffTheGivenUser()
  {
    await SetupWithAutentication();
    string userId = Context.Set<User>().Where(u => u.Id != AuthenticatedUser.Id).First().Id;

    var response = await GetManyAndValidateAsync($"?userId={userId}");
    Assert.True(response.Data.All(game => game.UserId == userId));
    Assert.True(response.Data.All(game => game.AnswerStatus == GameRequestAnswerStatus.Accepted.ToString()));
  }
}