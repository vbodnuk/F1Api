using Models.Models;
using Newtonsoft.Json;
using Serilog;

namespace Formula1ApiConnection.Repositories;


public static class F1ApiReader
{
    private static readonly HttpClient Client = new HttpClient();
    
    private static async Task<T> GetApiData<T>(string url) where T : new()
    {
        var methodString = url.Substring(url.LastIndexOf('/') + 1).Replace('-', ' ');
        try
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Log.Logger.Warning($"Can't get {methodString}! Status Code: {response.StatusCode}");

                return new T();
            }

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
        catch (Exception e)
        {
            Log.Logger.Error(e,$"Failed to get {methodString} data!");
            throw;
        }
    }

    public static async Task<ConstructorsResponseModel> GetConstructorsChampionshipByYearAsync(int year)
    {
        return await GetApiData<ConstructorsResponseModel>($"https://f1api.dev/api/{year}/constructors-championship");
    }

    public static async Task<ConstructorsResponseModel> GetCurrentConstructorsChampionshipAsync()
    {
        return await GetApiData<ConstructorsResponseModel>("https://f1api.dev/api/current/constructors-championship");
    }

    public static async Task<DriversChampionshipResponseModel> GetCurrentDriversChampionshipAsync()
    {
        return await GetApiData<DriversChampionshipResponseModel>("https://f1api.dev/api/current/drivers-championship");
    }

    public static async Task<DriversChampionshipResponseModel> GetDriversChampionshipByYearAsync(int year)
    {
        return await GetApiData<DriversChampionshipResponseModel>($"https://f1api.dev/api/{year}/drivers-championship");
    }

    public static async Task<RaceResponseModel> GetRacesResultsByYearByRoundAsync(int year, int round)
    {
        return await GetApiData<RaceResponseModel>($"https://f1api.dev/api/{year}/{round}/race");
    }

    public static async Task<RaceResponseModel> GetCurrentRaceResultAsync()
    {
        return await GetApiData<RaceResponseModel>("https://f1api.dev/api/current/last/race");
    }

    public static async Task<DriversResponseModel> GetDriversByYear(int year)
    {
        return await GetApiData<DriversResponseModel>($"https://f1api.dev/api/{year}/drivers");
    }

    public static async Task<DriversResponseModel> GetCurrentDriversAsync()
    {
        return await GetApiData<DriversResponseModel>("https://f1api.dev/api/drivers");
    }
    
}