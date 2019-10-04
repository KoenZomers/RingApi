using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class MotionZone
    {
        [JsonProperty("enable_audio", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableAudio { get; set; }

        [JsonProperty("active_motion_filter", NullValueHandling = NullValueHandling.Ignore)]
        public long? ActiveMotionFilter { get; set; }

        [JsonProperty("sensitivity", NullValueHandling = NullValueHandling.Ignore)]
        public long? Sensitivity { get; set; }

        [JsonProperty("advanced_object_settings", NullValueHandling = NullValueHandling.Ignore)]
        public AdvancedObjectSettings AdvancedObjectSettings { get; set; }

        [JsonProperty("zone1", NullValueHandling = NullValueHandling.Ignore)]
        public Zone Zone1 { get; set; }

        [JsonProperty("zone2", NullValueHandling = NullValueHandling.Ignore)]
        public Zone Zone2 { get; set; }

        [JsonProperty("zone3", NullValueHandling = NullValueHandling.Ignore)]
        public Zone Zone3 { get; set; }

        [JsonProperty("pir_settings", NullValueHandling = NullValueHandling.Ignore)]
        public PirSettings PirSettings { get; set; }
    }
}
