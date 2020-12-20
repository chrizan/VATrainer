using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VATrainer.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual Question Question { get; set; }

        public virtual ICollection<ArticleAnswer> ArticleAnswers { get; set; } = new List<ArticleAnswer>();
    }
}
