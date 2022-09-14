using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class ChimeFeatures
    {
        [JsonPropertyName("ringtones_enabled")]
        public bool RingtonesEnabled { get; set; }
    }
}
