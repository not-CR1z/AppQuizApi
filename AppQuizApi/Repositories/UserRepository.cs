using AppQuizApi.Data;
using AppQuizApi.Domain.IRepository;
using AppQuizApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AppQuizApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task SaveUser(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ValidateUserExistence(User user)
        {
            return _context.Users.AnyAsync(u => u.UserName == user.UserName);
        }

        public async Task<User?> Login(User user)
        {
            var userRegistered = await _context.Users.Where(x => x.UserName == user.UserName && x.Password==user.Password).FirstOrDefaultAsync();
            if(userRegistered != null)
            {
               userRegistered.Avatar = await _context.Avatar.Where(x => x.Id == userRegistered.AvatarId).FirstOrDefaultAsync();
            }
            return userRegistered;
        }
        public async Task<User?> GetUserInfo(User user)
        {
            var userSelected = await _context.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).Include(x => x.Avatar).FirstOrDefaultAsync();
            return userSelected;
        }
        public async Task<User?> ValidatePassword(Int32 id, String password)
        {
            var userSelected =  await this._context.Users.Where(x => x.Id == id && x.Password == password).FirstOrDefaultAsync();
            return userSelected;
        }

        public async Task UpdatePassword(User user)
        {
            this._context.Update(user);
            await this._context.SaveChangesAsync();
        }

        public async Task<Avatar?> GetAvatarByUser(int id)
        {
            return await _context.Avatar.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
