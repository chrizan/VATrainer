using Microsoft.EntityFrameworkCore;
using VATrainer.Models;
using Xamarin.Forms;

namespace VATrainer.DataLayer
{
    public class VATrainerContext : DbContext
    {
        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleQuestion> ArticleQuestion { get; set; }
        public DbSet<ArticleAnswer> ArticleAnswer { get; set; }
        public DbSet<Theme> Theme { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleQuestion>()
                .HasKey(a => new { a.ArticleId, a.QuestionId });
            modelBuilder.Entity<ArticleAnswer>()
                .HasKey(a => new { a.ArticleId, a.AnswerId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = DatabaseConstants.DbFileName;
            if (Device.Android == Device.RuntimePlatform || Device.iOS == Device.RuntimePlatform)
            {
                dbPath = DependencyService.Get<IDatabaseService>().GetDbPath();
            }
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
