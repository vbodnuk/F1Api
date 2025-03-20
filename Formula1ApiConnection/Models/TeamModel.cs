using Newtonsoft.Json;

namespace Formula1ApiConnection.Models;

public class TeamModel
{
    [JsonProperty("teamId")]
    public string TeamId { get; set; }
    
    [JsonProperty("teamName")]
    public string TeamName { get; set; }
    
    [JsonProperty("teamNationality")]
    public string TeamNationality { get; set; }
    
    [JsonProperty("firstAppeareance")]
    public int? FirstAppeareance { get; set; }
    
    [JsonProperty("constructorsChampionships")]
    public string ConstructorChampionship { get; set; }
    
    [JsonProperty("driversChampionships")]
    public string DriverChampionship { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; }
}