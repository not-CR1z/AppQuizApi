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
        public async Task<IActionResult> GetQuizzes()
        {
            var quizes = await _quizService.GetQuizzes();
            return Ok(quizes);
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
            return Ok(new { message = "La pregunta se ha agregado correctamente al examen" });
        }

        [HttpPost("getQuizzesByUser")]
        public async Task<IActionResult> GetQuizzesByUser([FromBody] int userId)
        {
            var quizzesFound = await _quizService.GetQuizzesByUser(userId);
            return Ok(new { quizzes = quizzesFound });
        }

        [HttpPost("deleteQuiz")]
        public async Task<IActionResult> DeleteQuiz([FromBody]int quizId)
        {
            await _quizService.DeleteQuiz(quizId);
            return Ok(new { message = "Tu quiz se ha removido con éxito" });
        }
        [HttpPost("getQuizById")]
        public async Task<IActionResult> GetQuestionsByQuiz([FromBody] int quizId)
        {
            var quizToPresent = await _quizService.GetQuestionsByQuiz(quizId);
            if(quizToPresent != null)
            {
                return Ok(quizToPresent);
            }
            return BadRequest("Ocurrió un error al obtener el examen");
        }
        [HttpPost("updateQuiz")]
        public async Task<IActionResult> UpdateQuiz(Quiz quiz)
        {
            var wasUpdated = await _quizService.UpdateQuiz(quiz);
            if (wasUpdated)
            {
                return Ok(new {message="Quiz actualizado con éxito"});
            }
            return BadRequest("No se ha podido realizar la actualización");
        }

        [HttpPost("updateQuestion")]
        public async Task<IActionResult> UpdateQuestion(Question question)
        {
            var wasUpdated = await _quizService.UpdateQuestion(question);
            if (wasUpdated)
            {
                return Ok(new { message = "Pregunta actualizado con éxito" });
            }
            return BadRequest("No se ha podido realizar la actualización");
        }
        [HttpPost("deleteQuestion")]
        public async Task<IActionResult> DeleteQuestion([FromBody] int questionId)
        {
            var wasDeleted= await _quizService.DeleteQuestion(questionId);
            if (wasDeleted)
            {
                return Ok(new { message = "Pregunta eliminada con éxito" });
            }
            return BadRequest("No se ha podido realizar la acción");
        }
    }
}
