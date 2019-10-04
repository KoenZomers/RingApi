using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class HumanDetectionConfidence
    {
        [JsonProperty("day", NullValueHandling = NullValueHandling.Ignore)]
        public double? Day { get; set; }

        [JsonProperty("night", NullValueHandling = NullValueHandling.Ignore)]
        public double? Night { get; set; }
    }
}
