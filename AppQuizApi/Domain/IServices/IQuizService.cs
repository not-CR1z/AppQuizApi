using AppQuizApi.Domain.Models;

namespace AppQuizApi.Domain.IServices
{
    public interface IQuizService
    {
        Task<List<Quiz>> GetQuizzes();
        Task AddQuiz(Quiz quiz);
    }
}
