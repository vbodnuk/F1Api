using DataBase;
using DataBase.Models;
using Formula1ApiConnection.Utils;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Formula1ApiConnection.Repositories;

public class DatabaseWriter
{
    private readonly F1DbContext _f1DbContext;

    public DatabaseWriter(F1DbContext dwhContext)
    {
        _f1DbContext = dwhContext;
    }

    public async Task WriteAllRacesAsync()
    {
        int startYear = 2015;
        var year = DateTime.Now.Year + 1;
        
        while (startYear < year)
        {
            int emptyRace = 0;
            int round = 1;

            while(emptyRace < 2)
            {
                var response = await F1ApiReader.GetRacesResultsByYearByRoundAsync(startYear, round);

                if (response?.Races == null || response.Races.Results.Count == 0)
                {
                    Log.Logger.Warning($"No results for {year} round {round}!!");
                    emptyRace++;
                    round++;
                    continue;
                }

                try
                {
                    var circuitId = response.Races.Circuit.CircuitId;
                    var raceResult = response.Races.Results
                        .DistinctBy(d=>d.Driver.DriverId)
                        .Select(r => new RaceResultEntity()
                    {
                        Position = StringToIntParser.Parse(r.Position),
                        Points = r.Points,
                        Round = round,
                        Year = startYear,
                        Time = r.Time,
                        DriverId = r.Driver.DriverId,
                        CircuitId = circuitId,
                        FastLap = r.FastLap,
                        TeamId = r.Team.TeamId,
                        Grid = StringToIntParser.Parse(r.Grid)
                    }).ToList();

                    await _f1DbContext.RaceResults.AddRangeAsync(raceResult);
                    await _f1DbContext.SaveChangesAsync();

                    emptyRace = 0;
                    Log.Logger.Information($"Races Result written for year {year} round {round}");
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e, "Data weren't wrote in DB");
                }

                round++;
            }

            startYear++;
        }
    }

    public async Task WriteCurrentRaceResultAsync()
    {
        try
        {
            var response = await F1ApiReader.GetCurrentRaceResultAsync();

            if (response.Races != null)
            {
                var round = response.Races.Round;
                var circuitId = response.Races.Circuit.CircuitId;
                var raceResult = response.Races.Results.Select(r => new RaceResultEntity()
                {
                    Position = StringToIntParser.Parse(r.Position),
                    Points = r.Points,
                    Round = round,
                    Time = r.Time,
                    DriverId = r.Driver.DriverId,
                    CircuitId = circuitId,
                    FastLap = r.FastLap,
                    TeamId = r.Team.TeamId,
                    Year = DateTime.Now.Year,
                    Grid = StringToIntParser.Parse(r.Grid)
                }).ToList();

                await _f1DbContext.UpsertRacesAsync(raceResult);
                
                Log.Logger.Information($"Races Result written for current Year round:{round}");
            }

        }
        catch (Exception e)
        {
            Log.Logger.Error(e,"Data weren't wrote in DB");
            throw;
        }
        
    }
}