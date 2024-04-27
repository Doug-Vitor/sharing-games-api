using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Tests.App.Controllers;

public abstract class BaseTest : IClassFixture<WebApplicationFactory<Program>>
{
  protected readonly WebApplicationFactory<Program> Factory;
  protected readonly HttpClient Client;

  public BaseTest(string baseAddress)
  {
    Factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
      builder.UseEnvironment(Environments.Staging));
    Client = Factory.CreateClient(new() { BaseAddress = new(baseAddress) });
  }

  protected async Task<T> GetAndParseAsync<T>(string? requestUrl = "")
  {
    HttpResponseMessage response = await Client.GetAsync(requestUrl);
    return await ParseAsync<T>(response);
  }

  protected async Task<T> PostAndParseAsync<T>(object body = null, string? requestUrl = "")
  {
    HttpResponseMessage response = await Client.PostAsJsonAsync(requestUrl, body);
    return await ParseAsync<T>(response);
  }

  async Task<T> ParseAsync<T>(HttpResponseMessage response)
  {
    string responseBody = await response.Content.ReadAsStringAsync();
    return responseBody.FromJson<T>();
  }
}