using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class LightSettings
    {
        [JsonPropertyName("brightness")]
        public long? Brightness { get; set; }
    }
}
