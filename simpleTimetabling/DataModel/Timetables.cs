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

        //[JsonProperty(PropertyName = "Name")]
        //public string Name { get; set; }
        //[JsonProperty(PropertyName = "Day")]
        //public string Day { get; set; }
        //[JsonProperty(PropertyName = "Lecture")]
        //public string Lecture { get; set; }
        //[JsonProperty(PropertyName = "Lecturer")]
        //public string Lecturer { get; set; }
        //[JsonProperty(PropertyName = "Time")]
        //public string Time { get; set; }
    }
}
