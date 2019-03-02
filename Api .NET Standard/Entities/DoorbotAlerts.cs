using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class DoorbotAlerts
    {
        [JsonProperty(PropertyName = "connection")]
        public string Connection { get; set; }
    }
}
