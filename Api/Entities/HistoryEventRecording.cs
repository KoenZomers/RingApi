using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class HistoryEventRecording
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
