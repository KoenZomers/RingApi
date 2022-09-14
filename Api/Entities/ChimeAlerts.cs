using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class ChimeAlerts
    {
        [JsonPropertyName("connection")]
        public string Connection { get; set; }
    }
}
