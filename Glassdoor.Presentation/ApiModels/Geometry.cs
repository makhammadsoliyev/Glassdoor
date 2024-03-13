using Newtonsoft.Json;

public class Geometry
{
    [JsonProperty("lat")]
    public string Latitude { get; set; }

    [JsonProperty("lng")]
    public string Longitude { get; set; }
}