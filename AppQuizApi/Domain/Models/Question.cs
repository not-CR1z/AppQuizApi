using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppQuizApi.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [ForeignKey("QuizId")]
        public int QuizId { get; set; }
        public ICollection<Answer>? Answers { get; set; }
    }
}
