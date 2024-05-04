using System.Net;
using Core.Response;

namespace Tests.App.Controllers.V1;

public abstract class BaseTest<TViewModel>(string controllerName) : AuthenticatedBaseTest($"/api/v1/{controllerName}")
{
  protected async Task<SuccessResponse<TViewModel>> GetSingleAndValidateAsync(string? requestUrl = "")
  {
    var response = await GetAndParseAsync<SuccessResponse<TViewModel>>(requestUrl);

    Assert.Equal(response.StatusCode, (int)HttpStatusCode.OK);
    return response;
  }

  protected async Task<SuccessResponse<IEnumerable<TViewModel>>> GetManyAndValidateAsync(string? requestUrl = "")
  {
    var response = await GetAndParseAsync<SuccessResponse<IEnumerable<TViewModel>>>(requestUrl);

    Assert.Equal(response.StatusCode, (int)HttpStatusCode.OK);
    Assert.NotEmpty(response.Data);
    return response;
  }

  protected async Task<SuccessResponse<TViewModel>> PostAndValidatAsync(object body = null, string? requestUrl = "")
  {
    var response = await PostAndParseAsync<SuccessResponse<TViewModel>>(body, requestUrl);

    Assert.Equal(response.StatusCode, (int)HttpStatusCode.Created);
    return response;
  }

  protected async Task<SuccessResponse<TViewModel>> UpdateAndValidateAsync(object body = null, string? requestUrl = "")
  {
    var response = await PatchAndParseAsync<SuccessResponse<TViewModel>>(body, requestUrl);

    Assert.Equal(response.StatusCode, (int)HttpStatusCode.OK);
    return response;
  }

  protected async Task<ActionResponse> DeleteAndValidateAsync(string? requestUrl = "")
  {
    var response = await GetAndParseAsync<ActionResponse>(requestUrl);
    Assert.Equal(response.StatusCode, (int)HttpStatusCode.NoContent);
    return response;
  }
}