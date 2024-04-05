using JsLocalization.DAL;

namespace JsLocalization.Models
{
    public partial class Culture
    {
        public Culture()
        {
            DbLanguageResources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Resource> DbLanguageResources { get; set; }
    }
    public partial class Culture:BaseEntity
    {
        
    }
}
