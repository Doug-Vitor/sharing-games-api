

using Core.Entities;
using Core.V1.DTOs;

namespace Tests.App.Controllers.V1;

public abstract class BaseTest() : AuthenticatedBaseTest("/api/v1/Games")
{
  public void ApplyDefaultAsserts(Game comparingGame, GameViewModel viewModel)
  {
    Assert.Equal(comparingGame.Name, viewModel.Name);
    Assert.Equal(comparingGame.Synopsis, viewModel.Synopsis);
    Assert.Equal(comparingGame.PublisherId, viewModel.Publisher.Id);
    Assert.Equal(comparingGame.ReleasedAt, viewModel.ReleasedAt);
  }
}