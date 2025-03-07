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
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                NameSurname = model.FullName,
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "User created successfully.";
            else
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}";
                }
            return response; ;
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
            if(user != null)
            {
                var isValid = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "ResetPassword", resetToken);
                if (isValid)
                {
                   await _userManager.ResetPasswordAsync(user, resetToken,newPassword);
                }

            }
        }
    }
}
