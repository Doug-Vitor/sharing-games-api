using Core.Entities.Request;
using Core.V1.DTOs;

namespace Tests.App.Controllers.V1.Suggestions;

public abstract class BaseTest() : BaseTest<Suggestion, RequestViewModel>("suggestions");