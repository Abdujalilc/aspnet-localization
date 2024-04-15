using JsLocalizeText.DAL;

namespace JsLocalizeText.Models
{
    public partial class Culture
    {
        public Culture()
        {
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
    public partial class Culture:BaseEntity
    {
        
    }
}
