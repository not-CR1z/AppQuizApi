using AppQuizApi.Domain.Models;

namespace AppQuizApi.Domain.IServices
{
    public interface IQuizService
    {
        Task<List<Quiz>> GetQuizzes();
        Task AddQuiz(Quiz quiz);
        Task<List<Category>> GetCategories();
        Task AddQuestion(Question question);
        Task<List<Quiz>> GetQuizzesByUser(int userId);

    }
}
