using AppQuizApi.Domain.Models;

namespace AppQuizApi.Domain.IRepositories
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetQuizzes();
    }
}
