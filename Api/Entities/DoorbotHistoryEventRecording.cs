using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class DoorbotHistoryEventRecording
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
