using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class StickupCamSettings
    {
        [JsonProperty("enable_vod")]
        public long EnableVod { get; set; }

        [JsonProperty("exposure_control")]
        public long ExposureControl { get; set; }

        [JsonProperty("motion_zones")]
        public long[] MotionZones { get; set; }

        [JsonProperty("motion_snooze_preset_profile")]
        public string MotionSnoozePresetProfile { get; set; }

        [JsonProperty("motion_snooze_presets")]
        public string[] MotionSnoozePresets { get; set; }

        [JsonProperty("live_view_preset_profile")]
        public string LiveViewPresetProfile { get; set; }

        [JsonProperty("live_view_presets")]
        public string[] LiveViewPresets { get; set; }

        [JsonProperty("pir_sensitivity_1")]
        public long PirSensitivity1 { get; set; }

        [JsonProperty("vod_suspended")]
        public long VodSuspended { get; set; }

        [JsonProperty("doorbell_volume")]
        public long DoorbellVolume { get; set; }

        [JsonProperty("vod_status")]
        public string VodStatus { get; set; }

        [JsonProperty("advanced_motion_detection_enabled")]
        public bool AdvancedMotionDetectionEnabled { get; set; }

        [JsonProperty("advanced_motion_zones")]
        public AdvancedMotionZones AdvancedMotionZones { get; set; }

        [JsonProperty("advanced_motion_detection_human_only_mode")]
        public bool AdvancedMotionDetectionHumanOnlyMode { get; set; }

        [JsonProperty("enable_audio_recording")]
        public object EnableAudioRecording { get; set; }

        [JsonProperty("light_settings")]
        public LightSettings LightSettings { get; set; }

        [JsonProperty("enable_white_leds")]
        public long EnableWhiteLeds { get; set; }
    }
}
