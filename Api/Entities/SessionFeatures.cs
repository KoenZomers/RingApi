
using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class SessionFeatures
    {
        [JsonProperty(PropertyName = "remote_logging_format_storing")]
        public bool RemoteLoggingFormatStoring { get; set; }

        [JsonProperty(PropertyName = "remote_logging_level")]
        public int RemoteLoggingLevel { get; set; }

        [JsonProperty(PropertyName = "subscriptions_enabled")]
        public bool SubscriptionsEnabled { get; set; }

        [JsonProperty(PropertyName = "stickupcam_setup_enabled")]
        public bool StickupcamSetupEnabled { get; set; }

        [JsonProperty(PropertyName = "vod_enabled")]
        public bool VodEnabled { get; set; }

        [JsonProperty(PropertyName = "nw_enabled")]
        public bool NwEnabled { get; set; }

        [JsonProperty(PropertyName = "nw_user_activated")]
        public bool NwUserActivated { get; set; }

        [JsonProperty(PropertyName = "ringplus_enabled")]
        public bool RingPlusEnabled { get; set; }

        [JsonProperty(PropertyName = "lpd_enabled")]
        public bool LdpEnabled { get; set; }

        [JsonProperty(PropertyName = "reactive_snoozing_enabled")]
        public bool ReactiveSnoozingEnabled { get; set; }

        [JsonProperty(PropertyName = "proactive_snoozing_enabled")]
        public bool ProActiveSnoozingEnabled { get; set; }

        [JsonProperty(PropertyName = "owner_proactive_snoozing_enabled")]
        public bool OwnerProActiveSnoozingEnabled { get; set; }

        [JsonProperty(PropertyName = "live_view_settings_enabled")]
        public bool LiveviewSettingsEnabled { get; set; }

        [JsonProperty(PropertyName = "delete_all_settings_enabled")]
        public bool DeleteAllSettingsEnabled { get; set; }

        [JsonProperty(PropertyName = "power_cable_enabled")]
        public bool PowerCableEnabled { get; set; }

        [JsonProperty(PropertyName = "device_health_alerts_enabled")]
        public bool DeviceHealthAlertsEnabled { get; set; }

        [JsonProperty(PropertyName = "chime_pro_enabled")]
        public bool ChimeProEnabled { get; set; }

        [JsonProperty(PropertyName = "multiple_calls_enabled")]
        public bool MultipleCallsEnabled { get; set; }

        [JsonProperty(PropertyName = "ujet_enabled")]
        public bool UjetEnabled { get; set; }

        [JsonProperty(PropertyName = "multiple_delete_enabled")]
        public bool MultipleDeleteEnabled { get; set; }

        [JsonProperty(PropertyName = "delete_all_enabled")]
        public bool DeleteAllEnabled { get; set; }

        [JsonProperty(PropertyName = "lpd_motion_announcement_enabled")]
        public bool LdpMotionAnnouncementEnabled { get; set; }

        [JsonProperty(PropertyName = "starred_events_enabled")]
        public bool StarredEventsEnabled { get; set; }

        [JsonProperty(PropertyName = "chime_dnd_enabled")]
        public bool ChimeDndEnabled { get; set; }

        [JsonProperty(PropertyName = "video_search_enabled")]
        public bool VideoSearchEnabled { get; set; }

        [JsonProperty(PropertyName = "floodlight_cam_enabled")]
        public bool FlooglightCamEnabled { get; set; }

        [JsonProperty(PropertyName = "nw_larger_area_enabled")]
        public bool NwLargerAreaEnabled { get; set; }

        [JsonProperty(PropertyName = "ring_cam_battery_enabled")]
        public bool RingCamBatteryEnabled { get; set; }

        [JsonProperty(PropertyName = "elite_cam_enabled")]
        public bool EliteCamEnabled { get; set; }

        [JsonProperty(PropertyName = "doorbell_v2_enabled")]
        public bool DoorbellV2Enabled { get; set; }

        [JsonProperty(PropertyName = "spotlight_battery_dashboard_controls_enabled")]
        public bool SpotlightBatteryDashboardControlsEnabled { get; set; }

        [JsonProperty(PropertyName = "bypass_account_verification")]
        public bool ByPassAccountVerification { get; set; }

        [JsonProperty(PropertyName = "legacy_cvr_retention_enabled")]
        public bool LegacyCvrRetentionEnabled { get; set; }

        [JsonProperty(PropertyName = "new_dashboard_enabled")]
        public bool NewDashboardEnabled { get; set; }

        [JsonProperty(PropertyName = "ring_cam_enabled")]
        public bool RingCamEnabled { get; set; }
    }
}
