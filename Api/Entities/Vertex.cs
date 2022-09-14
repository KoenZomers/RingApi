using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class Vertex
    {
        [JsonPropertyName("x")]
        public long? X { get; set; }

        [JsonPropertyName("y")]
        public long? Y { get; set; }
    }
}
