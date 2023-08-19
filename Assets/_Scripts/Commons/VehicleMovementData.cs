using Commons.Types;
using Newtonsoft.Json;

namespace Commons
{
    public class VehicleMovementData
    {
        [JsonProperty("CurrentLocation")] public Coordinate CurrentLocation { get; set; }

        [JsonProperty("CurrentOrientationAngle")]
        public float CurrentOrientationAngle { get; set; }

        [JsonProperty("IsBot")] public bool IsBot { get; set; }
        [JsonProperty("TargettingMcp")] public Models.Mcp TargettingMcp { get; set; }

        [JsonProperty("MapboxDirectionResponse")]
        public MapboxDirectionResponse MapboxDirectionResponse { get; set; }
    }
}