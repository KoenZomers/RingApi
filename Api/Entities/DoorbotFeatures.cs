using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class DoorbotFeatures
    {
        [JsonProperty(PropertyName = "motions_enabled")]
        public bool MotionsEnabled { get; set; }

        [JsonProperty(PropertyName = "show_recordings")]
        public bool ShowRecordings { get; set; }

        [JsonProperty(PropertyName = "advanced_motion_enabled")]
        public bool AdvancedMotionEnabled { get; set; }

        [JsonProperty(PropertyName = "people_only_enabled")]
        public bool PeopleOnlyEnabled { get; set; }

        [JsonProperty(PropertyName = "shadow_correction_enabled")]
        public bool ShadowCorrectionEnabled { get; set; }

        [JsonProperty(PropertyName = "motion_message_enabled")]
        public bool MotionMessageEnabled { get; set; }

        [JsonProperty(PropertyName = "night_vision_enabled")]
        public bool NightVisionEnabled { get; set; }
    }
}
