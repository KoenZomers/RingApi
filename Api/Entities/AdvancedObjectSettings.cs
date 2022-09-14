using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class AdvancedObjectSettings
    {
        [JsonProperty("human_detection_confidence", NullValueHandling = NullValueHandling.Ignore)]
        public HumanDetectionConfidence HumanDetectionConfidence { get; set; }

        [JsonProperty("motion_zone_overlap", NullValueHandling = NullValueHandling.Ignore)]
        public HumanDetectionConfidence MotionZoneOverlap { get; set; }

        [JsonProperty("object_time_overlap", NullValueHandling = NullValueHandling.Ignore)]
        public HumanDetectionConfidence ObjectTimeOverlap { get; set; }

        [JsonProperty("object_size_minimum", NullValueHandling = NullValueHandling.Ignore)]
        public HumanDetectionConfidence ObjectSizeMinimum { get; set; }

        [JsonProperty("object_size_maximum", NullValueHandling = NullValueHandling.Ignore)]
        public HumanDetectionConfidence ObjectSizeMaximum { get; set; }
    }
}
