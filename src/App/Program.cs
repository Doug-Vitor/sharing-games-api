using App.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDefaultConfigurations();

var app = builder.Build();
app.UseDefaultConfigurations();
app.Run();
