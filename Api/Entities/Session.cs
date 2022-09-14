using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class Session
    {
        [JsonPropertyName("profile")]
        public Profile Profile { get; set; }
    }
}