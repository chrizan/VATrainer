using System.ComponentModel.DataAnnotations;

namespace VATrainer.Models
{
    public class Theme
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Text { get; set; }
    }
}
