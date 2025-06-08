using Cronos;
using DataBase;
using Formula1ApiConnection.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Formula1ApiConnection.Services;

public class DatabaseSyncService : BackgroundService
{
    private readonly F1DbContext _f1DbContext;
    private readonly DatabaseWriter _databaseWriter;
    private static readonly CronExpression _cron = CronExpression.Parse("46 15 * * *");
    
    public DatabaseSyncService(F1DbContext f1DbContext, DatabaseWriter databaseWriter)
    {
        _f1DbContext = f1DbContext;
        _databaseWriter = databaseWriter;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("DataBaseSyncerServer has been started!");
        while (!stoppingToken.IsCancellationRequested)
        {
            var next = _cron.GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Utc);
            if (next.HasValue)
            {
                var delay = next.Value - DateTime.UtcNow;
                await Task.Delay(delay, stoppingToken);

                try
                {
                    //await RacesSyncTaskAsync();
                    await DriversSyncTaskAsync();
                }
                catch (Exception e)
                {
                    Log.Logger.Warning("Exception: ",e);
                }
            }
        }
    }

    private async Task RacesSyncTaskAsync()
    {
        var lastSync = await _f1DbContext.RaceResults.AnyAsync();

        if (lastSync)
        {
            await _databaseWriter.WriteCurrentRaceResultAsync();
        }
        else
        {
            await _databaseWriter.WriteAllRacesAsync();
        }
    }

    private async Task DriversSyncTaskAsync()
    {
        var lastSync = await _f1DbContext.Drivers.AnyAsync();
        
        if (lastSync)
        {
            await _databaseWriter.WriteCurrentDrivers();
        }
        else
        {
            await _databaseWriter.WriteDrivers();
        }
    }


}