using AppQuizApi.Data;
using AppQuizApi.Domain.IRepositories;
using AppQuizApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AppQuizApi.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDBContext _context;
        public QuizRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task AddQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Quiz>> GetQuizzes()
        {
            return await _context.Quizzes.ToListAsync();
        }
    }
}
