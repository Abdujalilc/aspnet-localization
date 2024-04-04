using JsLocalization.DAL;

namespace JsLocalization.Models
{
    public partial class SpLanguage
    {
        public SpLanguage()
        {
            DbLanguageResources = new HashSet<DbLanguageResource>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<DbLanguageResource> DbLanguageResources { get; set; }
    }
    public partial class SpLanguage:BaseEntity
    {
        
    }
}
