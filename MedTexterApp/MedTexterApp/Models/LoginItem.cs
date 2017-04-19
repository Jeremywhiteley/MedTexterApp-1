using Newtonsoft.Json;

namespace MedTexterApp
{
    // model that will bind the following properties with the DocumentDB
    public class LoginItem
    {
        [JsonProperty(PropertyName = "FacebookId")]
        public string FacebookId { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
    }
}
