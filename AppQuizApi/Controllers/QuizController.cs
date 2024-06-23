using AppQuizApi.Domain.IServices;
using AppQuizApi.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppQuizApi.Controllers

//Clase encargada del manejo de las peticiones que incluyan quizzes, preguntas y respuestas 
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

        //Controlador encargado de la creación de un nuevo Quiz
        [HttpPost("addQuiz")]
        public async Task<IActionResult> AddQuiz(Quiz quiz)
        {
            await _quizService.AddQuiz(quiz);
            return Ok(new { message = "Quiz guardado con éxito", QuizId = quiz.Id });
        }

        //Controlador encargado de la obtención de la lista de Quizzes creados
        [Authorize]
        [HttpPost("getQuizzes")]
        public async Task<IActionResult> GetQuizzes()
        {
            var quizes = await _quizService.GetQuizzes();
            return Ok(quizes);
        }

        //Controlador encargado de obtener las categorías previo a la creación de un Quiz
        [HttpPost("getCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var quizes = await _quizService.GetCategories();
            return Ok(quizes);
        }

        //Controlador encargado de la creación de una nueva pregunta y sus respectivas opciones
        [HttpPost("addQuestion")]
        public async Task<IActionResult> AddQuestion(Question question)
        {
            await _quizService.AddQuestion(question);
            return Ok(new { message = "La pregunta se ha agregado correctamente al examen" });
        }

        //Controlador encargado de la obtención de los Quizzes creados por el usuario logeado
        [HttpPost("getQuizzesByUser")]
        public async Task<IActionResult> GetQuizzesByUser([FromBody] int userId)
        {
            var quizzesFound = await _quizService.GetQuizzesByUser(userId);
            return Ok(new { quizzes = quizzesFound });
        }

        //Controlador encargado de la eliminación de un Quiz
        [HttpPost("deleteQuiz")]
        public async Task<IActionResult> DeleteQuiz([FromBody] int quizId)
        {
            await _quizService.DeleteQuiz(quizId);
            return Ok(new { message = "Tu quiz se ha removido con éxito" });
        }

        //Controlador encargado de la obtención de un Quiz en específico para su presentación o actualización
        [HttpPost("getQuizById")]
        public async Task<IActionResult> GetQuestionsByQuiz([FromBody] int quizId)
        {
            var quizToPresent = await _quizService.GetQuestionsByQuiz(quizId);
            if (quizToPresent != null)
            {
                return Ok(quizToPresent);
            }
            return BadRequest("Ocurrió un error al obtener el examen");
        }

        //Controlador encargado de la actualización de un Quiz específico
        [HttpPost("updateQuiz")]
        public async Task<IActionResult> UpdateQuiz(Quiz quiz)
        {
            var wasUpdated = await _quizService.UpdateQuiz(quiz);
            if (wasUpdated)
            {
                return Ok(new { message = "Quiz actualizado con éxito" });
            }
            return BadRequest("No se ha podido realizar la actualización");
        }

        //Controlador encargaado de la actualización de una pregunta en específico
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

        //Controlador encargado de eliminar una pregunta durante la actualización de un Quiz
        [HttpPost("deleteQuestion")]
        public async Task<IActionResult> DeleteQuestion([FromBody] int questionId)
        {
            var wasDeleted = await _quizService.DeleteQuestion(questionId);
            if (wasDeleted)
            {
                return Ok(new { message = "Pregunta eliminada con éxito" });
            }
            return BadRequest(new { message = "No se ha podido ejecutar la acción solicitada" });
        }

        //Controlador encargado de registrar el intento del usuario
        [HttpPost("presentQuiz")]
        public async Task<IActionResult> PresentQuiz([FromBody] int quizId)
        {
            var attempAdded = await _quizService.AddAttemp(quizId);
            if (attempAdded)
            {
                return Ok(new { message = "Se guardó el registro de la presentación del Quiz" });
            }
            return BadRequest(new { message = "No se ha podido ejecutar la acción solicitada" });
        }

        //Controlador encargado de almacenar la calificación de estrellas dada por el usuario
        [HttpPost("addStars")]
        public async Task<IActionResult> AddStars(Stats stats)
        {
            await _quizService.AddStats(stats);
            return Ok(new { message = "Se guardó tu calificación sobre el cuestionario" });
        }
    }
}
