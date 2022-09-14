using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KoenZomers.Ring.Api.Entities
{
    public class DoorbotHistoryEvent
    {
        /// <summary>
        /// Unique identifier of this historical event
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Raw date time string when this event occurred
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// DateTime at which this event occurred 
        /// </summary>
        private DateTime? _createdAtDateTime;
        public DateTime? CreatedAtDateTime
        {
            get
            {
                if (_createdAtDateTime.HasValue) return _createdAtDateTime.Value;

                if (!DateTime.TryParse(CreatedAt, out DateTime result))
                {
                    return null;
                }

                return _createdAtDateTime = result;
            }
        }

        /// <summary>
        /// Boolean indicating if the ring was answered
        /// </summary>
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
