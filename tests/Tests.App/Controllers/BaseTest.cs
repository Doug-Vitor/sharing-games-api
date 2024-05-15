using System.Net.Http.Json;
using Infra;
using Infra.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tests.App.Controllers;

public abstract class BaseTest : IClassFixture<WebApplicationFactory<Program>>
{
  protected readonly WebApplicationFactory<Program> Factory;
  protected readonly HttpClient Client;
  protected string BaseAddress;

  protected readonly AppDbContext Context;
  protected readonly AppDbSeeder Seeder;

  public BaseTest(string baseAddress)
  {
    Factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
      builder.UseEnvironment(Environments.Staging));

    BaseAddress = baseAddress;
    Client = Factory.CreateClient(new() { BaseAddress = new(baseAddress) });
    Context = Factory.Server.Services.GetRequiredService<AppDbContext>();
    Seeder = Factory.Server.Services.GetRequiredService<AppDbSeeder>();
  }

  protected async Task<T> GetAndParseAsync<T>(string? requestUrl = "")
  {
    HttpResponseMessage response = await Client.GetAsync(BaseAddress + requestUrl);
    return await ParseAsync<T>(response);
  }

  protected async Task<T> PostAndParseAsync<T>(object body = null, string? requestUrl = "")
  {
    HttpResponseMessage response = await Client.PostAsJsonAsync(BaseAddress + requestUrl, body);
    return await ParseAsync<T>(response);
  }

  protected async Task<T> PatchAndParseAsync<T>(object body = null, string? requestUrl = "")
  {
    HttpResponseMessage response = await Client.PatchAsJsonAsync(BaseAddress + requestUrl, body);
    return await ParseAsync<T>(response);
  }

  protected async Task<T> DeleteAndParseAsync<T>(string? requestUrl = "")
  {
    HttpResponseMessage response = await Client.DeleteAsync(BaseAddress + requestUrl);
    return await ParseAsync<T>(response);
  }

  protected async Task<T> ParseAsync<T>(HttpResponseMessage response)
  {
    string responseBody = await response.Content.ReadAsStringAsync();
    return responseBody.FromJson<T>();
  }
}