using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class HistoryEventRecording
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
