using Newtonsoft.Json;

namespace simpleTimetabling
{
    class Timetables
    {
        public string ID { get; set; }
        [JsonProperty(PropertyName = "UserID")]
        public string UserID { get; set; }
        [JsonProperty(PropertyName = "Monday")]
        public string Monday { get; set; }
        [JsonProperty(PropertyName = "Tuesday")]
        public string Tuesday { get; set; }
        [JsonProperty(PropertyName = "Wednesday")]
        public string Wednesday { get; set; }
        [JsonProperty(PropertyName = "Thursday")]
        public string Thursday { get; set; }
        [JsonProperty(PropertyName = "Friday")]
        public string Friday { get; set; }
        [JsonProperty(PropertyName = "Saturday")]
        public string Saturday { get; set; }
        [JsonProperty(PropertyName = "Sunday")]
        public string Sunday { get; set; }
    }
}
