using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class DoNotDisturb
    {
        [JsonProperty(PropertyName = "seconds_left")]
        public int SecondsLeft { get; set; }
    }
}
