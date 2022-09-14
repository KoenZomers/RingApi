using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class DoorbotAlerts
    {
        [JsonPropertyName("connection")]
        public string Connection { get; set; }
    }
}
