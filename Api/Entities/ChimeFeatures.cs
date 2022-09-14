using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class ChimeFeatures
    {
        [JsonProperty(PropertyName = "ringtones_enabled")]
        public bool RingtonesEnabled { get; set; }
    }
}
