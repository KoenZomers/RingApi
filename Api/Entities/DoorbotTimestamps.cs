using Newtonsoft.Json;
using System.Collections.Generic;

namespace KoenZomers.Ring.Api.Entities
{
    /// <summary>
    /// Timestamps related to a specific doorbot
    /// </summary>
    public class DoorbotTimestamps
    {
        /// <summary>
        /// Collection of doorbot timestamps
        /// </summary>
        [JsonProperty(PropertyName = "timestamps")]
        public List<DoorbotTimestamp> Timestamp { get; set; }
    }
}
