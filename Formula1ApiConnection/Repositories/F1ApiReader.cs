using System.Text.Json;
using Formula1ApiConnection.Models;
using Newtonsoft.Json;
using Serilog;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace Formula1ApiConnection.Repositories;

public class F1ApiReader
{
    private static readonly HttpClient Client = new HttpClient();
    
    public async Task<List<TeamModel>> GetTeams(string url)
    {
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

    public async Task<List<ChampionshipModel>> GetSeasons(string url)
    {
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

    public async Task<List<ConstructorsResponseModel>> GetConstructors()
    {
        try
        {
            const string url = "https://f1api.dev/api/2024/constructors-championship";
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Failed to get constructors data! Status Code: {response.StatusCode}");

                return new List<ConstructorsResponseModel>();
            }

            var result = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<List<ConstructorsResponseModel>>(result)!.ToList();
            
            return models;
        }
        catch(Exception e)
        {
            Log.Logger.Error(e,"Can't get constructors data.");
            throw;
        }
    }

    public async Task<List<ConstructorsResponseModel>> GetConstructorsByYear(string year)
    {
        var url = $"https://f1api.dev/api/{year}/constructors-championship";
        
        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Failed to get constructors data! Status Code: {response.StatusCode}");

                return new List<ConstructorsResponseModel>();
            }

            var result = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<List<ConstructorsResponseModel>>(result)!.ToList();
            
            return models;
        }
        catch(Exception e)
        {
            Log.Logger.Error(e,"Can't get constructors data.");
            throw;
        }
    }
}