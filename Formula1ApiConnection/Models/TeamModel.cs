using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Formula1ApiConnection.Models;

public class TeamModel
{
    [JsonIgnore]
    public string TeamNationality { get; set; }
    
    [JsonProperty("teamId")]
    public string TeamId { get; set; }
    
    [JsonProperty("teamName")]
    public string TeamName { get; set; }
    
    [JsonProperty("teamNationality")]
    private string TeamNationalitySetter {
        set => TeamNationality = value;
    }
    
    [JsonProperty("country")]
    private string Country
    {
        set => TeamNationality = value;
    }
    
    [JsonProperty("firstAppeareance")]
    public int? FirstAppeareance { get; set; }
    
    [JsonProperty("constructorsChampionships")]
    public string ConstructorChampionship { get; set; }
    
    [JsonProperty("driversChampionships")]
    public string DriverChampionship { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; }
    
}