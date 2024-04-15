namespace JsLocalizeText.ViewModels
{
    public class DataTableSearchModel
    {
        public DataTableInputParams dataTableParams { get; set; }
    }
    public class DataTableInputParams
    {
        public string search { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public string sortColumn { get; set; }
        public string sortColumnDir { get; set; }
    }

    public class DataTableOutputParams<T>
    {
        public string draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public List<T> data { get; set; }
    }
}
