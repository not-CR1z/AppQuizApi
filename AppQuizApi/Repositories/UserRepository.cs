using AppQuizApi.Data;
using AppQuizApi.Domain.IRepository;
using AppQuizApi.Domain.Models;
using AppQuizApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AppQuizApi.Repositories
{
    //Clase encargada de las interacciones con las tablas de Usuario
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        //Método encargado de registrar un nuevo usuario
        public async Task SaveUser(User user)
        {
            user.AvatarId = 1;
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        //Método encrgado de verificar que el usuario a crear no sea un usuario ya registrado
        public Task<bool> ValidateUserExistence(User user)
        {
            return _context.Users.AnyAsync(u => u.UserName == user.UserName);
        }

        //Método encargado de la autenticación del usuario
        public async Task<User?> Login(User user)
        {
            var userRegistered = await _context.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefaultAsync();
            if (userRegistered != null)
            {
                userRegistered.Avatar = await _context.Avatar.Where(x => x.Id == userRegistered.AvatarId).FirstOrDefaultAsync();
            }
            return userRegistered;
        }

        //Método encargado de obtener la información del usuario logeado
        [Authorize]
        public async Task<User?> GetUserInfo(User user)
        {
            var userSelected = await _context.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).Include(x => x.Avatar).FirstOrDefaultAsync();
            return userSelected;
        }

        //Método encargado de hacer la actualización de contraseña
        [Authorize]
        public async Task<bool> UpdatePassword(ChangePasswordDto changePasswordDto)
        {
            var userSelected = await this._context.Users.Where(x => x.Id == changePasswordDto.UserId && x.Password == changePasswordDto.CurrentPassword).FirstOrDefaultAsync();
            if (userSelected != null)
            {
                userSelected.Password = changePasswordDto.NewPassword;
                await this._context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //Método encargado de obtener el avatar actual del usuario
        [Authorize]
        public async Task<Avatar?> GetAvatarByUser(int id)
        {
            return await _context.Avatar.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Método encargado de obtener la lista de avatars disponibles
        [Authorize]
        public Task<List<Avatar>> GetAvatars()
        {
            return _context.Avatar.ToListAsync();
        }

        //Método encargado de la actualización del avatar del usuario
        [Authorize]
        public async Task<bool> UpdateAvatar(User user)
        {
            var userToUpdate = await _context.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            if (userToUpdate != null)
            {
                userToUpdate.AvatarId = user.AvatarId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
