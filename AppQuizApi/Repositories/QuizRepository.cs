using AppQuizApi.Data;
using AppQuizApi.Domain.IRepositories;
using AppQuizApi.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AppQuizApi.Repositories
{
    //Clase encargada de las interacciones con las tablas de Quizzes, preguntas y respuestas
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDBContext _context;
        public QuizRepository(AppDBContext context)
        {
            _context = context;
        }

        //Método encargado de la creación de un nuevo Quiz
        [Authorize]
        public async Task AddQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
        }

        //Método encargado de la creación de una nueva pregunta
        [Authorize]
        public async Task AddQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        //Método encargado de la obtención de los quizzes creados por todos los usuarios
        [Authorize]
        public async Task<List<Quiz>> GetQuizzes()
        {
            var quizzes = await _context.Quizzes.
                Include(x => x.Category).
                Include(x => x.Stats).
                Include(x => x.Creator).
                ThenInclude(x => x.Avatar).
                OrderByDescending(x => x.Id).
                ToListAsync();
            foreach (Quiz quiz in quizzes)
            {
                quiz.Creator!.Password = "";
            }
            return quizzes;
        }

        //Método encargadoo de obtener las categorías disponibles
        [Authorize]
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        //Método encargado de obtener los quizzes del usuario loggeado
        [Authorize]
        public async Task<List<Quiz>> GetQuizzesByUser(int userId)
        {
            return await _context.Quizzes.
                Where(x => x.CreatorId == userId).
                Include(x => x.Category).
                OrderByDescending(x => x.Id).
                ToListAsync();
        }

        //Método encargado de eliminar el Quiz seleccionado
        [Authorize]
        public async Task DeleteQuiz(int quizId)
        {
            var quizToDelete = _context.Quizzes.SingleOrDefault(x => x.Id == quizId);
            if (quizToDelete != null)
            {
                _context.Quizzes.Remove(quizToDelete);
            }
            await _context.SaveChangesAsync();
        }

        //Método encargado de obtener la lista de preguntas dado el Quiz seleccionado
        [Authorize]
        public async Task<Quiz?> GetQuestionsByQuiz(int quizId)
        {
            return await _context.Quizzes.
                Where(x => x.Id == quizId).
                Include(x => x.Questions)!.
                ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync();
        }

        //Método encargado de la actualización del Quiz seleccionado
        [Authorize]
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

        //Método encargado de la actualización de la pregunta seleccionada
        [Authorize]
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

        //Método encargado de la eliminación de la pregunta seleccionada
        [Authorize]
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

        //Método encargado de registrar el intento del usuario
        [Authorize]
        public async Task<bool> AddAttemp(int quizId)
        {
            var quizPresented = await _context.Quizzes.Where(x => x.Id == quizId).FirstOrDefaultAsync();
            if (quizPresented != null)
            {
                quizPresented.Attemps++;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //Método encargado de guardar la calificación de estrellas del usuario
        [Authorize]
        public async Task AddStats(Stats stats)
        {
            _context.Stats.Add(stats);
            await _context.SaveChangesAsync();
        }
    }
}
