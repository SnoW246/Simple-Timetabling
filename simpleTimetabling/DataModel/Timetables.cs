using System;
using Newtonsoft.Json;

namespace simpleTimetabling
{
    class Timetables
    {
        public string ID { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Day")]
        public string Day { get; set; }
        [JsonProperty(PropertyName = "Lecture")]
        public string Lecture { get; set; }
        [JsonProperty(PropertyName = "Lecturer")]
        public string Lecturer { get; set; }
        [JsonProperty(PropertyName = "Time")]
        public string Time { get; set; }
    }
}
