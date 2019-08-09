using Newtonsoft.Json;
using System;

namespace KoenZomers.Ring.Api.Entities
{
    public class StickupCam
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("subscribed_motions")]
        public bool SubscribedMotions { get; set; }

        [JsonProperty("battery_life")]
        public long BatteryLife { get; set; }

        [JsonProperty("external_connection")]
        public bool ExternalConnection { get; set; }

        [JsonProperty("firmware_version")]
        public string FirmwareVersion { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("latitude")]
        public long Latitude { get; set; }

        [JsonProperty("longitude")]
        public long Longitude { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("settings")]
        public StickupCamSettings Settings { get; set; }

        [JsonProperty("features")]
        public StickupCamFeatures Features { get; set; }

        [JsonProperty("owned")]
        public bool Owned { get; set; }

        [JsonProperty("alerts")]
        public StickupCamAlerts Alerts { get; set; }

        [JsonProperty("motion_snooze")]
        public object MotionSnooze { get; set; }

        [JsonProperty("stolen")]
        public bool Stolen { get; set; }

        [JsonProperty("location_id")]
        public Guid LocationId { get; set; }

        [JsonProperty("ring_id")]
        public object RingId { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("battery_life_2")]
        public long BatteryLife2 { get; set; }

        [JsonProperty("battery_voltage")]
        public long BatteryVoltage { get; set; }

        [JsonProperty("battery_voltage_2")]
        public long BatteryVoltage2 { get; set; }

        [JsonProperty("led_status")]
        public DeviceStatus LedStatus { get; set; }

        [JsonProperty("siren_status")]
        public DeviceStatus SirenStatus { get; set; }

        [JsonProperty("night_mode")]
        public long NightMode { get; set; }
    }
}
