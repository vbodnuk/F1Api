using Newtonsoft.Json;

namespace Models.Models;

public class DriversApiModel
{
    [JsonProperty("driverId")]
    public string DriverId { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("surname")]
    public string Surname { get; set; }
    
    [JsonProperty("nationality")]
    public string Nationality { get; set; }
    
    [JsonProperty("birthday")]
    public string BirthDay { get; set; }
    
    [JsonProperty("number")]
    public int Number { get; set; }
    
    [JsonProperty("shortName")]
    public string ShortName { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; }
}

public class DriversResponseModel : MainApiResponseModel
{
    [JsonProperty("season")]
    public int Season { get; set; }
    
    [JsonProperty("drivers")]
    public List<DriversApiModel> Drivers { get; set; }
}