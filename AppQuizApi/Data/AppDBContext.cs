using AppQuizApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AppQuizApi.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions{ get; set; }
        public DbSet<Answer> Answers{ get; set; }
    }
}
