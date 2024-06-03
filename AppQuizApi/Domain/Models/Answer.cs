﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppQuizApi.Domain.Models
{
    public class Answer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public bool IsTrue { get; set; }
        public int? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }
    }
}
