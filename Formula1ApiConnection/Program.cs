using Formula1ApiConnection.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddScoped<F1ApiReader>();

var app = builder.Build();

app.Run();