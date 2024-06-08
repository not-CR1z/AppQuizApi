﻿using AppQuizApi.Domain.Models;
using AppQuizApi.Dtos;

namespace AppQuizApi.Domain.IRepository
{
    public interface IUserRepository
    {
        Task SaveUser(User user);
        Task<Boolean> ValidateUserExistence(User user);
        Task<User?> Login(User user);
        Task<bool> UpdatePassword(ChangePasswordDto user);
        Task<List<Avatar>> GetAvatars();
        Task<bool> UpdateAvatar(User user);
    }
}
