using Models.Models;
using Newtonsoft.Json;
using Serilog;

namespace Formula1ApiConnection.Repositories;


public class F1ApiReader
{
    private static readonly HttpClient Client = new HttpClient();
    
    public async Task<List<TeamModel>> GetTeams()
    {
        const string url = "";
        
        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Failed to get teams data! Status Code: {response.StatusCode}");
                return new List<TeamModel>();
            }

            var result = await response.Content.ReadAsStringAsync();

            var teams = JsonConvert.DeserializeObject<List<TeamModel>>(result)!.ToList();

            return teams;
        }
        catch (Exception e)
        {
            Log.Logger.Error(e,"Can't get teams data!");
            throw;
        }

    }
    public static async Task<ConstructorsResponseModel?> GetConstructorsChampionshipByYearAsync(int year)
    {
        try
        {
            var url = $"https://f1api.dev/api/{year}/constructors-championship";
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Can't get constructors championship! Status Code: {response.StatusCode}");

                return new ConstructorsResponseModel();
            }

            var result = await response.Content.ReadAsStringAsync();
            var constructors = JsonConvert.DeserializeObject<ConstructorsResponseModel>(result);
            
            return constructors;
        }
        catch(Exception e)
        {
            Log.Logger.Error(e,"Failed to get constructors data!");
            throw;
        }
    }
    
    public static async Task<ConstructorsResponseModel?> GetCurrentConstructorsChampionshipAsync()
    {
        const string url = "https://f1api.dev/api/current/constructors-championship";

        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Can't get current constructors championship!  Status Code: {response.StatusCode}");

                return new ConstructorsResponseModel();
            }

            var result = await response.Content.ReadAsStringAsync();
            var constructors = JsonConvert.DeserializeObject<ConstructorsResponseModel>(result);
            
            return constructors;
        }
        catch(Exception e)
        {
            Log.Logger.Error(e,"Failed to get current constructors data!");
            throw;
        }
    }

    public static async Task<DriversResponseModel> GetCurrentDriversChampionshipAsync()
    {
        const string url = "https://f1api.dev/api/current/drivers-championship";

        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Can't get current drivers championship! Status Code: {response.StatusCode}");

                return new DriversResponseModel();
            }

            var result = await response.Content.ReadAsStringAsync();
            var drivers = JsonConvert.DeserializeObject<DriversResponseModel>(result);
            return drivers;
        }
        catch (Exception e)
        {
            Log.Logger.Error(e,"Failed to get current drivers championship data!");
            throw;
        }
    }
    
    public static async Task<DriversResponseModel?> GetDriversChampionshipByYearAsync(int year)
    {
        var url = $"https://f1api.dev/api/{year}/drivers-championship";

        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Can't get drivers championship! Status Code: {response.StatusCode}");
                return new DriversResponseModel();
            }

            var result = await response.Content.ReadAsStringAsync();
            var drivers = JsonConvert.DeserializeObject<DriversResponseModel>(result);

            return drivers;
        }
        catch (Exception e)
        {
            Log.Logger.Error(e,"Failed to get drivers championship data!");
            throw;
        }
    }

    public static async Task<CoreRaceResponseModel> GetRacesResultsByYearByRoundAsync(int year, int round)
    {
        var url = $"https://f1api.dev/api/{year}/{round}/race";

        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning(
                    $"Can't get race round {round} in year {year}! Status Code: {response.StatusCode}");
                return new CoreRaceResponseModel();
            }

            var result = await response.Content.ReadAsStringAsync();
            var races = JsonConvert.DeserializeObject<CoreRaceResponseModel>(result);

            return races;
        }
        catch (Exception e)
        {
            Log.Logger.Error(e,"Failed to get race result!");
            throw;
        }
    }

    public static async Task<CoreRaceResponseModel> GetCurrentRaceResultAsync()
    {
        const string  url = "https://f1api.dev/api/current/last/race";
        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Can't get current race result! Status Code: {response.StatusCode}");
                return new CoreRaceResponseModel();
            }

            var result = await response.Content.ReadAsStringAsync();
            var raceResult = JsonConvert.DeserializeObject<CoreRaceResponseModel>(result);

            return raceResult;
        }
        catch (Exception e)
        {
            Log.Logger.Error(e,"Failed to get current race result!");
            throw;
        }
    }
}