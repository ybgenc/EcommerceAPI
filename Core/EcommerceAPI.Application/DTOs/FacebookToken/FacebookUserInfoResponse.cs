using System.Text.Json.Serialization;

namespace EcommerceAPI.Application.DTOs.FacebookToken
{
    public class FacebookUserInfoResponse
    {
        [JsonPropertyName("id")]
        public string Id {  get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email {  get; set; }

    }
}
