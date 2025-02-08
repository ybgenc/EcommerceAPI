using System.Text.Json.Serialization;

namespace EcommerceAPI.Application.DTOs.FacebookToken
{
    public class FacebookAccessTokenValidation
    {
        [JsonPropertyName("data")]

        public FacebookAccessTokenValidationData Data { get; set; }

    }

    public class FacebookAccessTokenValidationData
    {
        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

    }



}
