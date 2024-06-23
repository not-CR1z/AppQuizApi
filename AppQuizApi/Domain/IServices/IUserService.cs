using AppQuizApi.Domain.Models;
using AppQuizApi.Dtos;

namespace AppQuizApi.Domain.IServices
{
    //Interface con los métodos a implementar por UserService
    public interface IUserService
    {
        Task SaveUser(User user);
        Task<Boolean> ValidateUserExistence(User user);
        Task<User?> Login(User user);
        Task<bool> UpdatePassword(ChangePasswordDto changePasswordDto);
        Task<List<Avatar>> GetAvatars();
        Task<bool> UpdateAvatar(User user);
    }
}
