using JsLocalization.ViewModels;

namespace JsLocalization.Services
{
    public interface IDataTableInputParamsService
    {
        DataTableInputParams ToModel(IFormCollection form);
    }
    public class DataTableInputParamsService : IDataTableInputParamsService
    {
        public DataTableInputParams ToModel(IFormCollection form)
        {
            DataTableInputParams model = new DataTableInputParams();

            var start = form["start"].FirstOrDefault();
            var length = form["length"].FirstOrDefault();
            string search = form["search[value]"].FirstOrDefault();
            var sortColumn = form["columns[" + form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = form["order[0][dir]"].FirstOrDefault();


            model.take = length != null ? Convert.ToInt32(length) : 0;
            model.skip = start != null ? Convert.ToInt32(start) : 0;
            model.search = search;
            model.sortColumn = sortColumn;
            model.sortColumnDir = sortColumnDir;
            return model;
        }
    }
}
