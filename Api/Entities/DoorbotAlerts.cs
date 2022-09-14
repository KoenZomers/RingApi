using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class StickupCamAlerts
    {
        [JsonPropertyName("connection")]
        public string Connection { get; set; }
    }
}
