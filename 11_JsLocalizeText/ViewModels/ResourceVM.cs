namespace JsLocalizeText.ViewModels
{
    public class SearchResourceVM : DataTableSearchModel
    {
        public int LanguageID { get; set; }
    }

    public class ResourceVM
    {
        public int ID { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
    }
}
