using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppQuizApi.Domain.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        public int Attemps { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public User? Creator { get; set; }
        public ICollection<Question>? Questions{ get; set; }
        public ICollection<Stats>? Stats{ get; set; }
    }
}
