using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class StickupCamAlerts
    {
        [JsonProperty(PropertyName = "connection")]
        public string Connection { get; set; }
    }
}
