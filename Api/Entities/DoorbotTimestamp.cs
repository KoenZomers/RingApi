using Newtonsoft.Json;
using System;

namespace KoenZomers.Ring.Api.Entities
{
    /// <summary>
    /// Timestamp related to a specific doorbot
    /// </summary>
    public class DoorbotTimestamp
    {
        /// <summary>
        /// The id of the doorbot to which this timestamp relates
        /// </summary>
        [JsonProperty(PropertyName = "doorbot_id")]
        public string DoorbotId { get; set; }

        /// <summary>
        /// The timestamp in seconds since January 1, 1970
        /// </summary>
        [JsonProperty(PropertyName = "timestamp")]
        public long? TimestampEpoch { get; set; }

        /// <summary>
        /// The Date and Time to which the TimestampEpoch translates
        /// </summary>
        public DateTime? Timestamp => !TimestampEpoch.HasValue? null : (DateTime?) new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(TimestampEpoch.Value).ToLocalTime();
    }
}
