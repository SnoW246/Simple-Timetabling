using Newtonsoft.Json;

namespace simpleTimetabling
{
    class Users
    {
        public string ID { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Surname")]
        public string Surname { get; set; }
        [JsonProperty(PropertyName = "DOB")]
        public string DOB { get; set; }
        [JsonProperty(PropertyName = "Username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
    }
}
