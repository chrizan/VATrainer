using System.ComponentModel.DataAnnotations.Schema;

namespace VATrainer.Models
{
    public class ArticleQuestion
    {
        public int ArticleId { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public virtual Article Article { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual Question Question { get; set; }
    }
}
