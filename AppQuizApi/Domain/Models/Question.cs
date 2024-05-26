namespace AppQuizApi.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public List<Answer> Answers { get; set; }
        
    }
}
