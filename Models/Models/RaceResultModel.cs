using Newtonsoft.Json;

namespace Models.Models;

public class RaceResultModel
{
    [JsonProperty("position")]
    public string Position { get; set; }

    [JsonProperty("points")]
    public double Points { get; set; }
    
    [JsonProperty("grid")]
    public string Grid { get; set; }
    
    [JsonProperty("time")]
    public string Time { get; set; }
    
    [JsonProperty("fastLap")]
    public string FastLap { get; set; }
    
    [JsonProperty("driver")]
    public DriversModel Driver { get; set; }
    
    [JsonProperty("team")]
    public TeamModel Team { get; set; }
    
}