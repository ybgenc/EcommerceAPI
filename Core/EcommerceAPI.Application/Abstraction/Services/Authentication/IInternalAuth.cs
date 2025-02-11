using token = EcommerceAPI.Application.DTOs;
namespace EcommerceAPI.Application.Abstraction.Services.Authentication
{
    public interface IInternalAuth
    {
        Task<token.Token> LoginAsync(string UsernameOrEmail, string Password);
        Task<token.Token> RefreshTokenLogin(string RefreshToken);
    }
}
