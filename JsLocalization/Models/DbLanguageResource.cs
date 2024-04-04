
using JsLocalization.DAL;

namespace JsLocalization.Models
{
    public partial class DbLanguageResource
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public int LangId { get; set; }

        public virtual SpLanguage Lang { get; set; }
    }
    public partial class DbLanguageResource: BaseEntity
    {
    }    
}
