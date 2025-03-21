using Newtonsoft.Json;

namespace Formula1ApiConnection.Models;

public class DriversChampionshipsModel
{
    [JsonProperty("classificationId")]
    public int ClassificationId { get; set; }
    
    [JsonProperty("driverId")]
    public string DriverId { get; set; }
    
    [JsonProperty("teamId")]
    public string TeamId { get; set; }
    
    [JsonProperty("points")]
    public int Points { get; set; }
    
    [JsonProperty("position")]
    public int Position { get; set; }
    
    [JsonProperty("wins")]
    public int Wins { get; set; }
    
    [JsonProperty("driver")]
    public DriversModel Driver { get; set; }
    
    [JsonProperty("team")]
    public TeamModel Team { get; set; }
}