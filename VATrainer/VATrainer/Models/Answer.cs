using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VATrainer.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public virtual ICollection<ArticleAnswer> ArticleAnswers { get; set; } = new List<ArticleAnswer>();
    }
}
