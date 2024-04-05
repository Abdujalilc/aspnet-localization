namespace JsLocalization.ViewModels
{
    public class SearchLanguageResourcesVM : DataTableSearchModel
    {
        public int LanguageID { get; set; }
    }

    public class ResourcesVM
    {
        public int ID { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
    }
}
