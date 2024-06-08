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
        public async Task AddQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Quiz>> GetQuizzes()
        {
            var quizzes = await _context.Quizzes.Include(x => x.Category).Include(x => x.Creator).ThenInclude(x => x.Avatar).ToListAsync();
            foreach (Quiz quiz in quizzes)
            {
                quiz.Creator.Password = "";
            }
            return quizzes;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Quiz>> GetQuizzesByUser(int userId)
        {
            return await _context.Quizzes.Where(x => x.CreatorId == userId).Include(x => x.Category).ToListAsync();
        }

        public async Task DeleteQuiz(int quizId)
        {
            var quizToDelete = _context.Quizzes.SingleOrDefault(x => x.Id == quizId);
            if (quizToDelete != null)
            {
                _context.Quizzes.Remove(quizToDelete);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Quiz?> GetQuestionsByQuiz(int quizId)
        {
            return await _context.Quizzes.Where(x => x.Id == quizId).Include(x => x.Questions).ThenInclude(x => x.Answers).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateQuiz(Quiz quiz)
        {
            var quizToUpdate = await _context.Quizzes.Where(x => x.Id == quiz.Id).FirstOrDefaultAsync();
            if (quizToUpdate != null)
            {
                quizToUpdate.Name = quiz.Name;
                quizToUpdate.Description = quiz.Description;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateQuestion(Question question)
        {
            var questionToUpdate = await _context.Questions.Where(x => x.Id == question.Id).FirstOrDefaultAsync();
            if (questionToUpdate != null)
            {
                questionToUpdate.Name = question.Name;
                questionToUpdate.Answers = question.Answers;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteQuestion(int questionId)
        {
            var questionToDelete = await _context.Questions.Where(x => x.Id == questionId).FirstOrDefaultAsync();
            if (questionToDelete != null)
            {
                _context.Questions.Remove(questionToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
