using EcommerceAPI.Application.Abstraction.Services.Authentication;

namespace EcommerceAPI.Application.Abstraction.Services
{
    public interface IAuthService : IExternalAuth, IInternalAuth
    {
        Task ResetPasswordAsync(string email);

    }
}
