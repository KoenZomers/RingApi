using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class Chime
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "device_id")]
        public string DeviceId { get; set; }

        [JsonProperty(PropertyName = "time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty(PropertyName = "firmware_version")]
        public string FirmwareVersion { get; set; }

        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "settings")]
        public ChimeSettings Settings { get; set; }

        [JsonProperty(PropertyName = "features")]
        public ChimeFeatures Features { get; set; }

        [JsonProperty(PropertyName = "owned")]
        public bool Owned { get; set; }

        [JsonProperty(PropertyName = "alerts")]
        public ChimeAlerts Alerts { get; set; }

        [JsonProperty(PropertyName = "do_not_disturb")]
        public DoNotDisturb DoNotDisturb { get; set; }

        [JsonProperty(PropertyName = "stolen")]
        public bool Stolen { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public Owner Owner { get; set; }
    }
}
