using Newtonsoft.Json;
using System.Collections.Generic;

namespace KoenZomers.Ring.Api.Entities
{
    public class Devices
    {
        [JsonProperty(PropertyName = "doorbots")]
        public List<Doorbot> Doorbots { get; set; }

        [JsonProperty(PropertyName = "chimes")]
        public List<Doorbot> Chimes { get; set; }
    }
}
