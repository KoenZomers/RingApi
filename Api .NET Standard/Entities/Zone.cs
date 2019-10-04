using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class Zone
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        public long? State { get; set; }

        [JsonProperty("vertex1")]
        public Vertex Vertex1 { get; set; }

        [JsonProperty("vertex2")]
        public Vertex Vertex2 { get; set; }

        [JsonProperty("vertex3")]
        public Vertex Vertex3 { get; set; }

        [JsonProperty("vertex4")]
        public Vertex Vertex4 { get; set; }

        [JsonProperty("vertex5")]
        public Vertex Vertex5 { get; set; }

        [JsonProperty("vertex6")]
        public Vertex Vertex6 { get; set; }

        [JsonProperty("vertex7")]
        public Vertex Vertex7 { get; set; }

        [JsonProperty("vertex8")]
        public Vertex Vertex8 { get; set; }
    }
}
