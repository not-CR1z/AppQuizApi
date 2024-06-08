using AppQuizApi.Domain.IRepository;
using AppQuizApi.Domain.IServices;
using AppQuizApi.Domain.Models;
using AppQuizApi.Dtos;
using System.Reflection.Metadata.Ecma335;

namespace AppQuizApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task SaveUser(User user)
        {
            await _userRepository.SaveUser(user);
        }

        public Task<bool> ValidateUserExistence(User user)
        {
            return _userRepository.ValidateUserExistence(user);
        }
        public Task<User?> Login(User user)
        {
            return _userRepository.Login(user);
        }

        public Task<bool> UpdatePassword(ChangePasswordDto changePasswordDto)
        {
           return _userRepository.UpdatePassword(changePasswordDto);
        }

        public Task<List<Avatar>> GetAvatars()
        {
            return _userRepository.GetAvatars();
        }

        public Task<bool> UpdateAvatar(User user)
        {
            return _userRepository.UpdateAvatar(user);
        }

    }
}
