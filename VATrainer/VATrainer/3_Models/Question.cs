using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VATrainer.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int AnswerId { get; set; }

        [Required]
        public int ThemeId { get; set; }

        [Required]
        public int Order { get; set; }

        [ForeignKey(nameof(AnswerId))]
        public virtual Answer Answer { get; set; }

        [ForeignKey(nameof(ThemeId))]
        public virtual Theme Theme { get; set; }

        public virtual ICollection<ArticleQuestion> ArticleQuestions { get; set; } = new List<ArticleQuestion>();
    }
}
