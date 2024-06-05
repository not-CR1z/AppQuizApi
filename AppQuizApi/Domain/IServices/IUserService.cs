using AppQuizApi.Domain.Models;
using AppQuizApi.Dtos;

namespace AppQuizApi.Domain.IServices
{
    public interface IUserService
    {
        Task SaveUser(User user);
        Task<bool> ValidateExistence(User user);
        Task<User?> Login(User user);
        //Task<User?> GetUserInfo(User user);

        //Pendientes
        Task<User?> ValidatePassword(Int32 id, String password);
        Task<bool> UpdatePassword(ChangePasswordDto changePasswordDto);
    }
}
