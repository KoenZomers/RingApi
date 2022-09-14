using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class HumanDetectionConfidence
    {
        [JsonPropertyName("day")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public double? Day { get; set; }

        [JsonPropertyName("night")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? Night { get; set; }
    }
}
