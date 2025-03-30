using Newtonsoft.Json;

namespace Models.Models;

public class DriversChampionshipsApiModel
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
    public int? Wins { get; set; }
    
    [JsonProperty("driver")]
    public DriversApiModel Driver { get; set; }
    
    [JsonProperty("team")]
    public TeamResponseModel TeamResponse { get; set; }
}

public class DriversChampionshipResponseModel: MainApiResponseModel
{
    [JsonProperty("season")]
    public int Season { get; set; }
    
    [JsonProperty("championshipId")]
    public string ChampionshipId { get; set; }
    
    [JsonProperty("drivers_championship")]
    public List<DriversChampionshipsApiModel> DriversChampionships { get; set; }
}