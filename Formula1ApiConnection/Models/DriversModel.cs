using Newtonsoft.Json;

namespace Formula1ApiConnection.Models;

public class DriversModel
{
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