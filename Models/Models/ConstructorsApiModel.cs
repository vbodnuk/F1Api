using Newtonsoft.Json;

namespace Models.Models;

public class ConstructorsApiModel
{
    [JsonProperty("classificationId")]
    public int ClassificationId { get; set; }
    
    [JsonProperty("teamId")]
    public string TeamId { get; set; }
    
    [JsonProperty("points")]
    public int Points { get; set; }
    
    [JsonProperty("position")]
    public int Position { get; set; }
    
    [JsonProperty("wins")]
    public int? Wins { get; set; }
    
    [JsonProperty("team")]
    public TeamResponseModel TeamResponse { get; set; }
}

public class ConstructorsResponseModel : MainApiResponseModel
{
    [JsonProperty("season")]
    public int Season { get; set; }
    
    [JsonProperty("championshipId")]
    public string ChampionshipId { get; set; }
    
    [JsonProperty("constructors_championship")]
    public List<ConstructorsApiModel> ConstructorsChampionship { get; set; }
}