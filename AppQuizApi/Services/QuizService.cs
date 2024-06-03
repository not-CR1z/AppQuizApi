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

        public Task AddQuiz(Quiz quiz)
        {
            return _quizRepository.AddQuiz(quiz);
        }

        public Task<List<Quiz>> GetQuizzes()
        {
            return  _quizRepository.GetQuizzes();
        }

        public  Task<List<Category>> GetCategories()
        {
            return _quizRepository.GetCategories();
        }

        public Task AddQuestion(Question question)
        {
            return _quizRepository.AddQuestion(question);
        }

        public Task<List<Quiz>> GetQuizzesByUser(int userId)
        {
            return _quizRepository.GetQuizzesByUser(userId);
        }

        public Task DeleteQuiz(int quizId)
        {
            return _quizRepository.DeleteQuiz(quizId);
        }
    }
}
