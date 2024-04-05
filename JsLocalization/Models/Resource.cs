
using JsLocalization.DAL;

namespace JsLocalization.Models
{
    public partial class Resource
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public int LangId { get; set; }

        public virtual Culture Lang { get; set; }
    }
    public partial class Resource: BaseEntity
    {
    }    
}
