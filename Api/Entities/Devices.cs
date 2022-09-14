using Newtonsoft.Json;
using System.Collections.Generic;

namespace KoenZomers.Ring.Api.Entities
{
    /// <summary>
    /// Contains a collection of Ring devices
    /// </summary>
    public class Devices
    {
        /// <summary>
        /// All Ring doorbots
        /// </summary>
        [JsonProperty(PropertyName = "doorbots")]
        public List<Doorbot> Doorbots { get; set; }

        /// <summary>
        /// All Authorized Ring doorbots
        /// </summary>
        [JsonProperty(PropertyName = "authorized_doorbots")]
        public List<Doorbot> AuthorizedDoorbots { get; set; }

        /// <summary>
        /// All Ring chimes
        /// </summary>
        [JsonProperty(PropertyName = "chimes")]
        public List<Chime> Chimes { get; set; }

        /// <summary>
        /// All Ring stickup cameras
        /// </summary>
        [JsonProperty(PropertyName = "stickup_cams")]
        public List<StickupCam> StickupCams { get; set; }
    }
}
