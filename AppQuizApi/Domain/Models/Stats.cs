using System.ComponentModel.DataAnnotations.Schema;

namespace AppQuizApi.Domain.Models
{
    public class Stats
    {
        public int Id { get; set; }
        [ForeignKey("QuizId")]
        public int QuizId { get; set; }
        public int StarRating { get; set; }
    }
}
