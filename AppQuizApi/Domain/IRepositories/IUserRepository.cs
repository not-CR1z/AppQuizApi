using AppQuizApi.Domain.Models;

namespace AppQuizApi.Domain.IRepository
{
    public interface IUserRepository
    {
        Task SaveUser(User user);
        Task<Boolean> ValidateUserExistence(User user);
        Task<User> GetUserInfo(User user);

        //Pendiente
        Task<User> ValidatePassword(Int32 id, String password);
        Task UpdatePassword(User user);
    }
}
