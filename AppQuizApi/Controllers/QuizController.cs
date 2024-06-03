using AppQuizApi.Domain.IServices;
using AppQuizApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppQuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }
        [HttpPost("addQuiz")]
        public async Task<IActionResult> AddQuiz(Quiz quiz)
        {
            await _quizService.AddQuiz(quiz);
            return Ok(new { message = "Quiz guardado con éxito", QuizId = quiz.Id });
        }
        [HttpPost("getQuizzes")]
        public async Task GetQuizzes()
        {
            var quizes = await _quizService.GetQuizzes();
        }
        [HttpPost("getCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var quizes = await _quizService.GetCategories();
            return Ok(quizes);
        }

        [HttpPost("addQuestion")]
        public async Task<IActionResult> AddQuestion(Question question)
        {
            await _quizService.AddQuestion(question);
            return Ok(new { message = "La pregunta se ha agregado correctamente al examen"});
        }

        [HttpPost("getQuizzesByUser")]
        public async Task<IActionResult> GetQuizzesByUser([FromBody]int userId)
        {
            var quizzesFound = await _quizService.GetQuizzesByUser(userId);
            return Ok(new { quizzes = quizzesFound });
        }
    }
}
