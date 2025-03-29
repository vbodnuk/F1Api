using Newtonsoft.Json;

namespace Models.Models;

public class RaceResponseModel
{
    [JsonProperty("round")]
    public int Round { get; set; }
    
    [JsonProperty("date")]
    public string Date { get; set; }
    
    [JsonProperty("raceId")]
    public string RaceId { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; }
    
    [JsonProperty("circuit")]
    public CircuitModel Circuit { get; set; }
    
    [JsonProperty("results")]
    public List<RaceResultModel> Results { get; set; }
}

public class CoreRaceResponseModel: ApiCoreResponseModel
{
    [JsonProperty("season")]
    public int Season { get; set; }
    
    [JsonProperty("races")]
    public RaceResponseModel Races { get; set; }
}

