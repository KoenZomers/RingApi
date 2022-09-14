using Newtonsoft.Json;

namespace KoenZomers.Ring.Api.Entities
{
    public class Profile
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "phone_number")]
        public object PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "authentication_token")]
        public string AuthenticationToken { get; set; }

        [JsonProperty(PropertyName = "features")]
        public SessionFeatures Features { get; set; }

        [JsonProperty(PropertyName = "app_brand")]
        public string AppBrand { get; set; }

        [JsonProperty(PropertyName = "user_flow")]
        public string UserFlow { get; set; }

        [JsonProperty(PropertyName = "explorer_program_terms")]
        public string ExplorerProgramTerms { get; set; }
    }
}
