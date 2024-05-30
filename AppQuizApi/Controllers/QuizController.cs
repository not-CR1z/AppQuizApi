using AppQuizApi.Domain.IServices;
using AppQuizApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(new { message = "Quiz guardado con éxito" });
        }
        [HttpPost("getQuizzes")]
        public async Task GetQuizzes()
        {
            var quizes = await _quizService.GetQuizzes();
        }
    }
}
