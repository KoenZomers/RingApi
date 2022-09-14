using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class StickupCamSettings
    {
        [JsonProperty("enable_vod", NullValueHandling = NullValueHandling.Ignore)]
        public long? EnableVod { get; set; }

        [JsonProperty("exposure_control", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExposureControl { get; set; }

        [JsonProperty("motion_zones", NullValueHandling = NullValueHandling.Ignore)]
        public MotionZone MotionZones { get; set; }

        [JsonProperty("motion_snooze_preset_profile", NullValueHandling = NullValueHandling.Ignore)]
        public string MotionSnoozePresetProfile { get; set; }

        [JsonProperty("motion_snooze_presets", NullValueHandling = NullValueHandling.Ignore)]
        public string[] MotionSnoozePresets { get; set; }

        [JsonProperty("live_view_preset_profile", NullValueHandling = NullValueHandling.Ignore)]
        public string LiveViewPresetProfile { get; set; }

        [JsonProperty("live_view_presets", NullValueHandling = NullValueHandling.Ignore)]
        public string[] LiveViewPresets { get; set; }

        [JsonProperty("pir_sensitivity_1", NullValueHandling = NullValueHandling.Ignore)]
        public long? PirSensitivity1 { get; set; }

        [JsonProperty("vod_suspended", NullValueHandling = NullValueHandling.Ignore)]
        public long? VodSuspended { get; set; }

        [JsonProperty("doorbell_volume", NullValueHandling = NullValueHandling.Ignore)]
        public long? DoorbellVolume { get; set; }

        [JsonProperty("vod_status", NullValueHandling = NullValueHandling.Ignore)]
        public string VodStatus { get; set; }

        [JsonProperty("advanced_motion_detection_enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AdvancedMotionDetectionEnabled { get; set; }

        [JsonProperty("advanced_motion_zones", NullValueHandling = NullValueHandling.Ignore)]
        public AdvancedMotionZones AdvancedMotionZones { get; set; }

        [JsonProperty("advanced_motion_detection_human_only_mode", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AdvancedMotionDetectionHumanOnlyMode { get; set; }

        [JsonProperty("enable_audio_recording", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableAudioRecording { get; set; }

        [JsonProperty("light_settings", NullValueHandling = NullValueHandling.Ignore)]
        public LightSettings LightSettings { get; set; }

        [JsonProperty("enable_white_leds", NullValueHandling = NullValueHandling.Ignore)]
        public long? EnableWhiteLeds { get; set; }
    }
}
