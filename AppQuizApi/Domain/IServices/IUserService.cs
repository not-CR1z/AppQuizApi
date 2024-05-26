﻿using AppQuizApi.Domain.Models;

namespace AppQuizApi.Domain.IServices
{
    public interface IUserService
    {
        Task SaveUser(User user);
        Task<Boolean> ValidateExistence(User user);

        Task<User> GetUserInfo(User user);

        //Pendientes
        Task<User> ValidatePassword(Int32 id, String password);
        Task UpdatePassword(User user);
    }
}