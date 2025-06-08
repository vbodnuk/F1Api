using DataBase;
using DataBase.Models;
using Formula1ApiConnection.Utils;
using Microsoft.EntityFrameworkCore;
using Models.Models;
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

            List<RaceResultEntity> races = new();
            while (emptyRace < 2)
            {
                try
                {
                    var response = await F1ApiReader.GetRacesResultsByYearByRoundAsync(startYear, round);

                    if (response?.Races == null || response.Races.Results.Count == 0)
                    {
                        Log.Logger.Warning($"No results for {startYear} round {round}!!");
                        emptyRace++;
                        round++;
                        continue;
                    }
                    
                    var circuitId = response.Races.Circuit.CircuitId;
                    var raceResult = response.Races.Results
                        .DistinctBy(d => d.Driver.DriverId)
                        .Select(race => ToRaceResultEntity(race, round, startYear, circuitId));
                    
                    races.AddRange(raceResult);
                    
                    emptyRace = 0;
                    Log.Logger.Information($"Races Result stored for year {startYear} round {round}");
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e, $"Missing data for {startYear} round {round}!");
                    emptyRace++;
                }

                round++;
            }

            if (races.Count != 0)
            {
                await _f1DbContext.RaceResults.AddRangeAsync(races);
                await _f1DbContext.SaveChangesAsync();
                Log.Logger.Information($"Saved {races.Count} race results for {startYear}");
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
                var raceResult = response.Races.Results
                    .Select(race => ToRaceResultEntity(race,round,DateTime.Now.Year,circuitId)).ToList();

                await _f1DbContext.UpsertRacesAsync(raceResult);
                
                Log.Logger.Information($"Races Result has been written for current Year round:{round}");
            }
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Data weren't wrote in DB");
            throw;
        }
    }

    public async Task WriteDrivers()
    {
        int startYear = 2015;
        int year = DateTime.Now.Year + 1;

        List<DriversEntity> driversList = new();
        try
        {
            while (startYear < year)
            {
                var drivers = await F1ApiReader.GetDriversByYear(startYear);
                var results = drivers.Drivers.Select(driver => ToDriversEntity(driver, startYear)).ToList();

                driversList.AddRange(results);
                startYear++;
            }

            await _f1DbContext.Drivers.AddRangeAsync(driversList);
            await _f1DbContext.SaveChangesAsync();
            
            Log.Logger.Information("Drivers has been written!");
        }
        catch (Exception e)
        {
            Log.Logger.Warning(e,"Data weren't wrote in DB");
        }
    }

    public async Task WriteCurrentDrivers()
    {
        try
        {
            var drivers = await F1ApiReader.GetCurrentDriversAsync();
            int year = DateTime.Now.Year;
            var results = drivers.Drivers.Select(driver => ToDriversEntity(driver, year)).ToList();

            await _f1DbContext.UpsertDriversAsync(results);
            
            Log.Logger.Information("Drivers has been updated!");

        }
        catch (Exception e)
        {
            Log.Logger.Warning(e,"Data weren't wrote in DB");
        }
    }


    private DriversEntity ToDriversEntity(DriversApiModel driver,int year)
    {
        return new DriversEntity()
        {
            Name = driver.Name,
            Nationality = driver.Nationality,
            DriverId = driver.DriverId,
            Surname = driver.Surname,
            Year = year,
            BirthDay = MyParsers.StringToDateTimeParser(driver.BirthDay).ToUniversalTime()
        };
    }
    private RaceResultEntity ToRaceResultEntity(RaceResultApiModel apiModel, int round, int year, string circuitId)
    {
        return new RaceResultEntity()
        {
            Position = MyParsers.StringToIntParser(apiModel.Position),
            Points = apiModel.Points,
            Round = round,
            Time = apiModel.Time,
            DriverId = apiModel.Driver.DriverId,
            CircuitId = circuitId,
            FastLap = apiModel.FastLap,
            TeamId = apiModel.TeamResponse.TeamId,
            Year = DateTime.Now.Year,
            Grid = MyParsers.StringToIntParser(apiModel.Grid)
        };
    }
}