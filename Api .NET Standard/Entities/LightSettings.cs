using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class LightSettings
    {
        [JsonProperty("brightness")]
        public long Brightness { get; set; }
    }
}
