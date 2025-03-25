using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Formula1ApiConnection.Models;

public class ConstructorsModel
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
    public TeamModel Team { get; set; }
}

public class ConstructorsResponseModel : ApiCoreResponseModel
{
    [JsonProperty("season")]
    public int Season { get; set; }
    
    [JsonProperty("championshipId")]
    public string ChampionshipId { get; set; }
    
    [JsonProperty("constructors_championship")]
    public List<ConstructorsModel> ConstructorsChampionship { get; set; }
}