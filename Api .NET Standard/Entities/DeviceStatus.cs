using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class DeviceStatus
    {
        /// <summary>
        /// Contains the status of a Ring device
        /// </summary>
        public partial class Status
        {
            [JsonProperty("seconds_remaining")]
            public long SecondsRemaining { get; set; }
        }
    }
}
