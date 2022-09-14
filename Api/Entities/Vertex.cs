using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class Vertex
    {
        [JsonProperty("x")]
        public long? X { get; set; }

        [JsonProperty("y")]
        public long? Y { get; set; }
    }
}
