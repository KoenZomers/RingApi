using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class Session
    {
        [JsonProperty(PropertyName = "profile")]
        public Profile Profile { get; set; }
    }
}