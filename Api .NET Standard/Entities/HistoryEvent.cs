using Newtonsoft.Json;
using System.Collections.Generic;

namespace KoenZomers.Ring.Api.Entities
{
    public class HistoryEvent
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "answered")]
        public bool Answered { get; set; }

        [JsonProperty(PropertyName = "events")]
        public List<object> Events { get; set; }

        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "favorite")]
        public bool Favorite { get; set; }

        [JsonProperty(PropertyName = "snapshot_url")]
        public string SnapshotUrl { get; set; }

        [JsonProperty(PropertyName = "recording")]
        public DoorbotHistoryEventRecording Recording { get; set; }

        [JsonProperty(PropertyName = "doorbot")]
        public Doorbot Doorbot { get; set; }
    }
}
