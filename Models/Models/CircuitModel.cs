using Newtonsoft.Json;

namespace Models.Models;

public class CircuitModel
{
    [JsonProperty("circuitId")]
    public string CircuitId { get; set; }
    
    [JsonProperty("circuitName")]
    public string CircuitName { get; set; }
    
    [JsonProperty("country")]
    public string Country { get; set; }
    
    [JsonProperty("city")]
    public string City { get; set; }
    
    [JsonProperty("circuitLength")]
    public string CircuitLength { get; set; }
    
    [JsonProperty("corners")]
    public int? Corners { get; set; }
    
    [JsonProperty("firstParticipationYear")]
    public int? FirstParticipationYear { get; set; }
    
    [JsonProperty("lapRecord")]
    public string? LapRecord { get; set; }
    
    [JsonProperty("fastestLapDriverId")]
    public string? FastestLapDriverId { get; set; }
    
    [JsonProperty("fastestLapTeamId")]
    public string? FastestLapTeamId { get; set; }
    
    [JsonProperty("fastestLapYear")]
    public int? FastestLapYear { get; set; }
}