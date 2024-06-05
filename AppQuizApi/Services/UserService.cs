using AppQuizApi.Domain.IRepository;
using AppQuizApi.Domain.IServices;
using AppQuizApi.Domain.Models;
using AppQuizApi.Dtos;

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

        public Task<bool> ValidateExistence(User user)
        {
            return _userRepository.ValidateUserExistence(user);
        }
        public Task<User?> Login(User user)
        {
            return _userRepository.Login(user);
        }

        //public async Task<User?> GetUserInfo(User user)
        //{
        //    return await _userRepository.GetUserInfo(user);
        //}

        public async Task<User?> ValidatePassword(Int32 idUsuario, String passwordAnterior)
        {
            return await _userRepository.ValidatePassword(idUsuario, passwordAnterior);
        }

        public async Task<bool> UpdatePassword(ChangePasswordDto changePasswordDto)
        {
           return await _userRepository.UpdatePassword(changePasswordDto);
        }
    }
}
