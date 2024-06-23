using AppQuizApi.Domain.Models;

namespace AppQuizApi.Domain.IRepositories
{
    //Interface con los métodos a implementar por QuizRepository
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetQuizzes();
        Task AddQuiz(Quiz quiz);
        Task<List<Category>> GetCategories();
        Task AddQuestion(Question question);
        Task<List<Quiz>> GetQuizzesByUser(int userId);
        Task DeleteQuiz(int quizId);
        Task<Quiz?> GetQuestionsByQuiz(int quizId);
        Task<bool> UpdateQuiz(Quiz quiz);
        Task<bool> UpdateQuestion(Question question);
        Task<bool> DeleteQuestion(int questionId);
        Task<bool> AddAttemp(int quizId);
        Task AddStats(Stats stats);
    }
}
