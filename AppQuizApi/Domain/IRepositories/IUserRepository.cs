using AppQuizApi.Domain.Models;
using AppQuizApi.Dtos;

namespace AppQuizApi.Domain.IRepository
{
    public interface IUserRepository
    {
        Task SaveUser(User user);
        Task<Boolean> ValidateUserExistence(User user);
        Task<User?> Login(User user);
        //Task<User?> GetUserInfo(User user);
        //Pendiente
        Task<User?> ValidatePassword(Int32 id, String password);
        Task<bool> UpdatePassword(ChangePasswordDto user);
    }
}
