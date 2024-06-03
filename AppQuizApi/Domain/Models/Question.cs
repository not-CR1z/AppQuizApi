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
        public int QuizId { get; set; }
        [ForeignKey("QuizId")]
        public Quiz? Quiz { get; set; }
    }
}
