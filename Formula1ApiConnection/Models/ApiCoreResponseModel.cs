using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Formula1ApiConnection.Models;


public class ApiCoreResponseModel
{
    [JsonProperty("api")]
    public string Api { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; }
    
    [JsonProperty("limit")]
    public int Limit { get; set; }
    
    [JsonProperty("offset")]
    public int Offset { get; set; }
    
    [JsonProperty("total")]
    public int Total { get; set; }
}