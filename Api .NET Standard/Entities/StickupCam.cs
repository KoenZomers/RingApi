using Newtonsoft.Json;
using System;

namespace KoenZomers.Ring.Api.Entities
{
    public class StickupCam
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("device_id", NullValueHandling = NullValueHandling.Ignore)]
        public string DeviceId { get; set; }

        [JsonProperty("time_zone", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone { get; set; }

        [JsonProperty("subscribed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Subscribed { get; set; }

        [JsonProperty("subscribed_motions", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SubscribedMotions { get; set; }

        [JsonProperty("battery_life", NullValueHandling = NullValueHandling.Ignore)]
        public long? BatteryLife { get; set; }

        [JsonProperty("external_connection", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ExternalConnection { get; set; }

        [JsonProperty("firmware_version", NullValueHandling = NullValueHandling.Ignore)]
        public string FirmwareVersion { get; set; }

        [JsonProperty("kind", NullValueHandling = NullValueHandling.Ignore)]
        public string Kind { get; set; }

        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Latitude { get; set; }

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Longitude { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty("settings", NullValueHandling = NullValueHandling.Ignore)]
        public StickupCamSettings Settings { get; set; }

        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public StickupCamFeatures Features { get; set; }

        [JsonProperty("owned", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Owned { get; set; }

        [JsonProperty("alerts", NullValueHandling = NullValueHandling.Ignore)]
        public StickupCamAlerts Alerts { get; set; }

        [JsonProperty("motion_snooze", NullValueHandling = NullValueHandling.Ignore)]
        public string MotionSnooze { get; set; }

        [JsonProperty("stolen", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Stolen { get; set; }

        [JsonProperty("location_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid LocationId { get; set; }

        [JsonProperty("ring_id", NullValueHandling = NullValueHandling.Ignore)]
        public string RingId { get; set; }

        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public Owner Owner { get; set; }

        [JsonProperty("battery_life_2", NullValueHandling = NullValueHandling.Ignore)]
        public long? BatteryLife2 { get; set; }

        [JsonProperty("battery_voltage", NullValueHandling = NullValueHandling.Ignore)]
        public long? BatteryVoltage { get; set; }

        [JsonProperty("battery_voltage_2", NullValueHandling = NullValueHandling.Ignore)]
        public long? BatteryVoltage2 { get; set; }

        [JsonProperty("led_status", NullValueHandling = NullValueHandling.Ignore)]
        public DeviceStatus LedStatus { get; set; }

        [JsonProperty("siren_status", NullValueHandling = NullValueHandling.Ignore)]
        public DeviceStatus SirenStatus { get; set; }

        [JsonProperty("night_mode", NullValueHandling = NullValueHandling.Ignore)]
        public long? NightMode { get; set; }
    }
}
