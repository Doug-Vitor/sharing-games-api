using System.Net;
using Core.Entities;
using Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Tests.App.Controllers.V1;

public abstract class BaseTest<T, TViewModel> : AuthenticatedBaseTest
  where T : BaseEntity where TViewModel : class
{
  protected readonly DbSet<T> DbSet;

  public BaseTest(string controllerName) : base($"/api/v1/{controllerName}")
  {
    DbSet = Context.Set<T>();
  }

  protected async Task<SuccessResponse<TViewModel>> GetSingleAndValidateAsync(string? requestUrl = "")
  {
    var response = await GetAndParseAsync<SuccessResponse<TViewModel>>(requestUrl);

    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    return response;
  }

  protected async Task<SuccessResponse<IEnumerable<TViewModel>>> GetManyAndValidateAsync(string? requestUrl = "")
  {
    var response = await GetAndParseAsync<SuccessResponse<IEnumerable<TViewModel>>>(requestUrl);

    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    Assert.NotEmpty(response.Data);
    return response;
  }

  protected async Task<SuccessResponse<TViewModel>> PostAndValidateAsync(object body = null, string? requestUrl = "")
  {
    var response = await PostAndParseAsync<SuccessResponse<TViewModel>>(body, requestUrl);

    Assert.Equal((int)HttpStatusCode.Created, response.StatusCode);
    return response;
  }

  protected async Task<SuccessResponse<TViewModel>> UpdateAndValidateAsync(string requestUrl, object body = null)
  {
    var response = await PatchAndParseAsync<SuccessResponse<TViewModel>>(body, requestUrl);

    Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
    return response;
  }

  protected async Task<ActionResponse> DeleteAndValidateAsync(string requestUrl)
  {
    var response = await GetAndParseAsync<ActionResponse>(requestUrl);
    Assert.Equal((int)HttpStatusCode.NoContent, response.StatusCode);
    return response;
  }
}