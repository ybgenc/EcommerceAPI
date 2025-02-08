using System.Text.Json.Serialization;

namespace EcommerceAPI.Application.DTOs.FacebookToken
{
    public class FacebookToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}
