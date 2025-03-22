using Formula1ApiConnection.GrpcServices;
using Formula1ApiConnection.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddScoped<F1ApiReader>();
builder.Services.AddGrpc();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseGrpcWeb();

app.MapGrpcService<F1GrpcService>().EnableGrpcWeb();
app.UseSerilogRequestLogging();

app.Run();