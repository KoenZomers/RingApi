using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    /// <summary>
    /// Message providing an URL where to download a shared recording from
    /// </summary>
    public class SharedRecording
    {
        /// <summary>
        /// Not sure what it would contain. Returned an empty string during my tests.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url{ get; set; }

        /// <summary>
        /// The URL where to download a shared recording from
        /// </summary>
        [JsonProperty(PropertyName = "wrapper_url")]
        public string WrapperUrl { get; set; }
    }
}