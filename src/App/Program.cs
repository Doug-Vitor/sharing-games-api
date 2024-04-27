using App.Configurations;

DotNetEnv.Env.Load("../../.env");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDefaultConfigurations();

var app = builder.Build();
app.UseDefaultConfigurations();
app.Run();

public partial class Program;