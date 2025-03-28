using Newtonsoft.Json;

namespace Formula1ApiConnection.Models;

public class ChampionshipModel
{
    [JsonProperty("championshipId")]
    public string ChampionshipId { get; set; }
    
    [JsonProperty("championshipName")]
    public string ChampionshipName { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; }
    
    [JsonProperty("year")]
    public int Year { get; set; }
}