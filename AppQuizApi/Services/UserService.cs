﻿using AppQuizApi.Domain.IRepository;
using AppQuizApi.Domain.IServices;
using AppQuizApi.Domain.Models;

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

        public async Task<User> GetUserInfo(User user)
        {
            return await _userRepository.GetUserInfo(user);
        }


        public async Task<User> ValidatePassword(Int32 idUsuario, String passwordAnterior)
        {
            return await _userRepository.ValidatePassword(idUsuario, passwordAnterior);
        }

        public async Task UpdatePassword(User usuario)
        {
            await _userRepository.UpdatePassword(usuario);
        }

    }
}