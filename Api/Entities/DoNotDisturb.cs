using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class DoNotDisturb
    {
        [JsonPropertyName("seconds_left")]
        public decimal? SecondsLeft { get; set; }
    }
}
