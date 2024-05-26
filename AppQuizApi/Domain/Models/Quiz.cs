using System.ComponentModel.DataAnnotations;

namespace AppQuizApi.Domain.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        public Category Category { get; set; }
        public int IdCreator { get; set; }
        public List<Question> Questions { get; set; }
    }
}
