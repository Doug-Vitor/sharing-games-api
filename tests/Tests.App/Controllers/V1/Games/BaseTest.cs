using Core.Entities;
using Core.V1.DTOs;

namespace Tests.App.Controllers.V1.Games;

public abstract class BaseTest() : BaseTest<Game, GameViewModel>("games");