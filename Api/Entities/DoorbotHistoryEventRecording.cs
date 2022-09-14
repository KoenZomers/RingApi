using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class DoorbotHistoryEventRecording
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
