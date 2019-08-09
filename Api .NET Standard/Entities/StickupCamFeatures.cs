using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class StickupCamFeatures
    {
        [JsonProperty("motions_enabled")]
        public bool MotionsEnabled { get; set; }

        [JsonProperty("show_recordings")]
        public bool ShowRecordings { get; set; }

        [JsonProperty("show_vod_settings")]
        public bool ShowVodSettings { get; set; }
    }
}
