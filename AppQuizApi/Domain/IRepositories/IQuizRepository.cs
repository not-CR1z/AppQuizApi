﻿using AppQuizApi.Domain.Models;

namespace AppQuizApi.Domain.IRepositories
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetQuizzes();
        Task AddQuiz(Quiz quiz);
        Task<List<Category>> GetCategories();
        Task AddQuestion(Question question);
    }
}
