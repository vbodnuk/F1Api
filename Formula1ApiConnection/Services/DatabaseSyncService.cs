using Cronos;
using DataBase;
using Formula1ApiConnection.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Formula1ApiConnection.Services;

public class DatabaseSyncService : BackgroundService
{
    private readonly F1DbContext _f1DbContext;
    private readonly DatabaseWriter _baseWriter;
    private static readonly CronExpression _cron = CronExpression.Parse("35 22 * * *");
    
    public DatabaseSyncService(F1DbContext f1DbContext, DatabaseWriter baseWriter)
    {
        _f1DbContext = f1DbContext;
        _baseWriter = baseWriter;
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
                    await RacesSyncAsync();
                }
                catch (Exception e)
                {
                    Log.Logger.Warning("Exception: ",e);
                }
            }
        }
    }

    public async Task RacesSyncAsync()
    {
        var lastSync = await _f1DbContext.RaceResults.OrderBy(e=>e.Year).FirstOrDefaultAsync();

        if (lastSync != null)
        {
            await _baseWriter.WriteCurrentRaceResultAsync();
        }
        else
        {
            await _baseWriter.WriteAllRacesAsync();
        }
    }


}