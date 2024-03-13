using Newtonsoft.Json;

public class ApiResponse
{
    [JsonProperty("documentation")]
    public string Documentation { get; set; }

    [JsonProperty("results")]
    public List<Location> Locations { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }
}
