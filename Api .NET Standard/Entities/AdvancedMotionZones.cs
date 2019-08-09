using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class AdvancedMotionZones
    {
        [JsonProperty("zone1")]
        public Zone Zone1 { get; set; }

        [JsonProperty("zone2")]
        public Zone Zone2 { get; set; }

        [JsonProperty("zone3")]
        public Zone Zone3 { get; set; }
    }
}
