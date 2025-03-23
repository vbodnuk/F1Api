using System.Text.Json;
using Formula1ApiConnection.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

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

    public async Task<List<ChampionshipModel>> GetSeasons()
    {
        const string url = "";
        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Failed to get seasons data! Status Code: {response.StatusCode}");

                return new List<ChampionshipModel>();
            }
            var result = await response.Content.ReadAsStringAsync();

            var championships = JsonConvert.DeserializeObject<List<ChampionshipModel>>(result)!.ToList();

            return championships;
        }
        catch (Exception e)
        {
            Log.Logger.Error(e,"Can't get seasons data!");
            throw;
        }

    }

    public static async Task<ConstructorsResponseModel?> GetConstructorsChampionshipByYear(int year)
    {
        try
        {
            var url = $"https://f1api.dev/api/{year}/constructors-championship";
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Failed to get constructors data! Status Code: {response.StatusCode}");

                return new ConstructorsResponseModel();
            }

            var result = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<ConstructorsResponseModel>(result);
            
            return models;
        }
        catch(Exception e)
        {
            Log.Logger.Error(e,"Can't get constructors data.");
            throw;
        }
    }
    
    public static async Task<ConstructorsResponseModel?> GetCurrentConstructorsChampionship()
    {
        const string url = "https://f1api.dev/api/current/constructors-championship";

        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Failed to get current constructors data! Status Code: {response.StatusCode}");

                return new ConstructorsResponseModel();
            }

            var result = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<ConstructorsResponseModel>(result);
            
            return models;
        }
        catch(Exception e)
        {
            Log.Logger.Error(e,"Can't get current constructors data.");
            throw;
        }
    }

    public static async Task<DriversResponseModel> GetCurrentDriversChampionship()
    {
        const string url = "https://f1api.dev/api/current/drivers-championship";

        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Failed to get current championship data! Status Code: {response.StatusCode}");

                return new DriversResponseModel();
            }

            var result = await response.Content.ReadAsStringAsync();
            //var jsonObj = JsonConvert.DeserializeObject<JObject>(result);
            var models = JsonConvert.DeserializeObject<DriversResponseModel>(result);

            //var models = jsonObj["drivers_championship"].ToObject<List<DriversChampionshipsModel>>();
            return models;
        }
        catch (Exception e)
        {
            Log.Logger.Error(e,"Can't get current championship data.");
            throw;
        }
    }
    
    public static async Task<DriversResponseModel> GetDriversChampionshipByYear(int year)
    {
        var url = $"https://f1api.dev/api/{year}/current/drivers-championship";

        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Failed to get driver championship data! Status Code: {response.StatusCode}");

                return new DriversResponseModel();
            }

            var result = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<DriversResponseModel>(result);

            return models;
        }
        catch (Exception e)
        {
            Log.Logger.Error(e,"Can't get driver championship data.");
            throw;
        }
        
    }
}