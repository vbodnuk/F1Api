using DataBase;
using Formula1ApiConnection;
using Formula1ApiConnection.GrpcServices;
using Formula1ApiConnection.Repositories;
using Formula1ApiConnection.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Models;
using Newtonsoft.Json;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var settingPath = Path.Combine(homePath, "settings.yaml");

builder.Configuration.AddYamlFile(settingPath, optional: false);
builder.Services.Configure<SettingsModels>(builder.Configuration.GetSection("F1ApiConnection"));

builder.Services.AddDbContext<F1DbContext>((serviceProvider, options) =>
    {
        var settings = serviceProvider.GetRequiredService<IOptions<SettingsModels>>().Value;
        var connectionString = settings.DwhConnectionString;
        
        options.UseSqlServer(connectionString, migration =>
            migration.MigrationsHistoryTable("F1ApiMigration", "f1"));
    });

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddScoped<F1ApiReader>();
builder.Services.AddGrpc();
builder.Services.AddScoped<DatabaseWriter>();
builder.Services.AddScoped<DatabaseSyncService>();
builder.Services.AddHostedService(provider => 
    provider.CreateScope().ServiceProvider.GetRequiredService<DatabaseSyncService>());


var app = builder.Build();

app.UseSerilogRequestLogging();

app.MapGrpcService<F1GrpcService>();
app.MapGet("/", () => "This is a gRPC server. Use a gRPC client to communicate.");

app.Run();