using Core.Entities;
using Core.V1.DTOs;

namespace Tests.App.Controllers.V1.Genres;

public abstract class BaseTest() : BaseTest<Genre, NamedViewModel>("genres");