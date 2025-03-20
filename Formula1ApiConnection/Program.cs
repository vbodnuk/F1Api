using Formula1ApiConnection.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddScoped<F1ApiReader>();

var app = builder.Build();

/*app.MapGet("/", async (F1ApiReader reader) =>
{
    var result = await F1ApiReader.GetConstructors();
    return Results.Ok(result);
});*/

app.Run();