using token = EcommerceAPI.Application.DTOs;

namespace EcommerceAPI.Application.Abstraction.Services.Authentication
{
    public interface IExternalAuth
    {
        Task<token.Token> GoogleLoginAsync(string IdToken);
        Task<token.Token> FacebookLoginAsync(string authToken);
    }
}
