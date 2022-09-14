using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class PirSettings
    {
        [JsonProperty("sensitivity1", NullValueHandling = NullValueHandling.Ignore)]
        public long? Sensitivity1 { get; set; }

        [JsonProperty("sensitivity2", NullValueHandling = NullValueHandling.Ignore)]
        public long? Sensitivity2 { get; set; }

        [JsonProperty("sensitivity3", NullValueHandling = NullValueHandling.Ignore)]
        public long? Sensitivity3 { get; set; }

        [JsonProperty("zone_mask", NullValueHandling = NullValueHandling.Ignore)]
        public long? ZoneMask { get; set; }
    }
}
