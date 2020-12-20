using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VATrainer.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Text { get; set; }

        public virtual ICollection<ArticleQuestion> ArticleQuestions { get; set; } = new List<ArticleQuestion>();

        public virtual ICollection<ArticleAnswer> ArticleAnswers { get; set; } = new List<ArticleAnswer>();
    }
}
