using EcommerceAPI.Application.DTOs.User;

namespace EcommerceAPI.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUser(CreateUser model);
    }
}
