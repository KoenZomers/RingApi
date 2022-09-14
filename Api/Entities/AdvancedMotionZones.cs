using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class AdvancedMotionZones
    {
        [JsonPropertyName("zone1")]
        public Zone Zone1 { get; set; }

        [JsonPropertyName("zone2")]
        public Zone Zone2 { get; set; }

        [JsonPropertyName("zone3")]
        public Zone Zone3 { get; set; }
    }
}
