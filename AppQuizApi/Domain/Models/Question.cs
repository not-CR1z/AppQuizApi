using System.ComponentModel.DataAnnotations.Schema;

namespace AppQuizApi.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Answer> Answers { get; set; }
        public int QuizId { get; set; }
        [ForeignKey("QuizId")]
        public Quiz? Quiz { get; set; }
    }
}
