using System.Net;
using Core.Response;
using Core.V1.DTOs;

namespace Tests.App.Controllers.V1.Suggestions;

public class CreateTests() : BaseTest()
{
  [Fact]
  public async Task WhenUnauthenticatedShouldReturnUnauthorized()
  {
    var response = await Client.GetAsync(BaseAddress);
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Fact]
  public async Task WhenInvalidBodyProvidedShouldReturnUnprocessableEntity()
  {
    await SetupWithAutentication();
    RequestInputModel inputModel = new() { Title = "", Description = "" };

    var response = await PostAndParseAsync<ErrorResponse>(inputModel);
    Assert.Equal((int)HttpStatusCode.UnprocessableEntity, response.StatusCode);
    Assert.Contains(nameof(RequestInputModel.Title), response.Messages);
    Assert.Contains(nameof(RequestInputModel.Description), response.Messages);
    Assert.NotEmpty(response.Messages.Values.SelectMany(errorDescription => errorDescription));
  }

  [Fact]
  public async Task WhenValidBodyProvidedShouldCreateTheSuggestion()
  {
    await SetupWithAutentication();
    RequestInputModel inputModel = new() { Title = "My awesome suggestion", Description = "This is an awesome suggestion!" };

    var response = await PostAndValidateAsync(inputModel);
    Assert.Equal(inputModel.Title, response.Data.Title);
    Assert.Equal(inputModel.Description, response.Data.Description);
    Assert.IsType<int>(response.Data.Id);
  }
}