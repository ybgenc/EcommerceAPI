using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.DTOs.User;
using EcommerceAPI.Application.Exceptions;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace EcommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<CreateUserResponse> CreateUser(CreateUser model)
        {
            var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);
            var existingUserByUsername = await _userManager.FindByNameAsync(model.UserName);

            if (existingUserByEmail != null || existingUserByUsername != null)
            {
                return new CreateUserResponse
                {
                    Succeeded = false,
                    Message = "This email or username is already in use."
                };
            }

            // Yeni kullanıcıyı oluştur
            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                NameSurname = model.FullName
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            var response = new CreateUserResponse
            {
                Succeeded = result.Succeeded
            };

            if (result.Succeeded)
            {
                response.Message = "User created successfully.";
            }
            else
            {
                response.Message = string.Join(" | ", result.Errors.Select(e => $"{e.Code} - {e.Description}"));
            }

            return response;
        }



        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenExpireDate, int addTimeOnAccessToken)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpireDate = accessTokenExpireDate.AddSeconds(addTimeOnAccessToken);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new UserNotFoundException();

        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var isValid = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "ResetPassword", resetToken);
                if (isValid)
                {
                    await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                }

            }
        }
    }
}
