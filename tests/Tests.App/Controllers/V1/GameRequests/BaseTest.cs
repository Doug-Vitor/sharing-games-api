using Core.Entities;
using Core.Entities.Request;
using Core.Enums;
using Core.V1.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Tests.App.Controllers.V1.GameRequests;

public abstract class BaseTest() : BaseTest<GameRequest, GameRequestViewModel>("GameRequests")
{
  protected override async Task SetupWithAutentication()
  {
    await base.SetupWithAutentication();
    await CustomSeedAsync();
  }

  async Task CustomSeedAsync()
  {
    GameRequest request = new()
    {
      Title = "This is an awesome game",
      Description = "Please, add this for me",
      GameUrl = "sharing-games.com",
      UserId = AuthenticatedUser.Id,
      GameId = (await Context.Set<Game>().FirstAsync()).Id
    };

    IEnumerable<GameRequest> requests = [request, request];

    await DbSet.AddRangeAsync([request, request]);
    await Context.AddAsync(new GameRequestAnswer() { GameRequestId = requests.First().Id });
    await Context.AddAsync(new GameRequestAnswer()
    {
      GameRequestId = requests.Last().Id,
      Status = GameRequestAnswerStatus.Finished
    });

    await Context.SaveChangesAsync();
  }
}