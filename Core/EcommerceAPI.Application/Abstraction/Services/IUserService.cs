using EcommerceAPI.Application.DTOs.User;
using EcommerceAPI.Domain.Entities.Identity;

namespace EcommerceAPI.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUser(CreateUser model);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenExpireDate, int addTimeOnAccessToken);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);

    }
}
