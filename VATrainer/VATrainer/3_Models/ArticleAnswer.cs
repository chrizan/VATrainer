using System.ComponentModel.DataAnnotations.Schema;

namespace VATrainer.Models
{
    public class ArticleAnswer
    {
        public int ArticleId { get; set; }

        public int AnswerId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public virtual Article Article { get; set; }

        [ForeignKey(nameof(AnswerId))]
        public virtual Answer Answer { get; set; }
    }
}
