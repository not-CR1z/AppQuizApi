using AppQuizApi.Domain.IRepositories;
using AppQuizApi.Domain.IServices;
using AppQuizApi.Domain.Models;

namespace AppQuizApi.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository; 
        }

        public async Task AddQuiz(Quiz quiz)
        {
            await _quizRepository.AddQuiz(quiz);
        }

        public async Task<List<Quiz>> GetQuizzes()
        {
            return await _quizRepository.GetQuizzes();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _quizRepository.GetCategories();
        }

        public Task AddQuestion(Question question)
        {
            return _quizRepository.AddQuestion(question);
        }
    }
}
