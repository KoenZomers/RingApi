using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class ChimeSettings
    {
        [JsonProperty(PropertyName = "volume")]
        public int Volume { get; set; }

        [JsonProperty(PropertyName = "ding_audio_user_id")]
        public string DingAudioUserId { get; set; }

        [JsonProperty(PropertyName = "ding_audio_id")]
        public string DingAudioId { get; set; }

        [JsonProperty(PropertyName = "motion_audio_user_id")]
        public string MotionAudioUserId { get; set; }

        [JsonProperty(PropertyName = "motion_audio_id")]
        public string MotionAudioId { get; set; }
    }
}
