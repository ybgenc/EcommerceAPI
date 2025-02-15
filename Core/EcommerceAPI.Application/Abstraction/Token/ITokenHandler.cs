using EcommerceAPI.Domain.Entities.Identity;

namespace EcommerceAPI.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccesstoken(int accessTokenExpireDate, AppUser appUser);
        string RefreshToken();

    }
}
