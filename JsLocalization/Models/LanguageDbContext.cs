using Microsoft.EntityFrameworkCore;

namespace JsLocalization.Models
{
    public class LanguageDbContext : DbContext
    {
        public LanguageDbContext(DbContextOptions<LanguageDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<DbLanguageResource> DbLanguageResources { get; set; }
        public DbSet<SpLanguage> SpLanguages { get; set; }
    }
}
