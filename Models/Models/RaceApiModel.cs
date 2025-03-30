using Newtonsoft.Json;

namespace Models.Models;

public class RaceApiModel
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
    public CircuitApiModel Circuit { get; set; }
    
    [JsonProperty("results")]
    public List<RaceResultApiModel> Results { get; set; }
}

public class RaceResponseModel: MainApiResponseModel
{
    [JsonProperty("season")]
    public int Season { get; set; }
    
    [JsonProperty("races")]
    public RaceApiModel Races { get; set; }
}

