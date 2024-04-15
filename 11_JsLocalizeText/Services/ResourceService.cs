using JsLocalizeText.DAL;
using JsLocalizeText.Models;
using JsLocalizeText.ViewModels;
using System.Text.Json;

namespace JsLocalizeText.Services
{
    public interface IResourceService
    {
        DataTableOutputParams<ResourceVM> ResourceList(SearchResourceVM dModel);
        IQueryable<Resource> GetAllAsIQueryable();
    }
    public class ResourceService : IResourceService
    {
        IRepository<Resource> _repository;

        public ResourceService(IRepository<Resource> repository)
        {
            _repository = repository;
        }
        public DataTableOutputParams<ResourceVM> ResourceList(SearchResourceVM dModel)
        {
            DataTableOutputParams<ResourceVM> rResult = new DataTableOutputParams<ResourceVM>();
            var lanRes = GetAllAsIQueryable().IncludeMultiple(p => p.Lang);

            if (!string.IsNullOrEmpty(dModel.dataTableParams.search))
                lanRes = lanRes.Where(x => x.KeyName.Contains(dModel.dataTableParams.search.ToLower()) || x.Value.ToLower().Contains(dModel.dataTableParams.search.ToLower()) ||
                                           x.Lang.Name.Contains(dModel.dataTableParams.search.ToLower()));

            if (!(string.IsNullOrEmpty(dModel.dataTableParams.sortColumn) && string.IsNullOrEmpty(dModel.dataTableParams.sortColumnDir)))
            {
                switch (dModel.dataTableParams.sortColumn)
                {
                    case "KeyName":
                        {
                            lanRes = dModel.dataTableParams.sortColumnDir == "asc" ? lanRes.OrderBy(x => x.KeyName) : lanRes.OrderByDescending(x => x.KeyName);
                            break;
                        }
                    case "Value":
                        {
                            lanRes = dModel.dataTableParams.sortColumnDir == "asc" ? lanRes.OrderBy(x => x.Value) : lanRes.OrderByDescending(x => x.Value);
                            break;
                        }
                    default:
                        {
                            lanRes = lanRes.OrderByDescending(x => x.Id);
                            break;
                        }
                }
            }

            rResult.recordsTotal = lanRes.Count();
            var _signed = lanRes.Skip(dModel.dataTableParams.skip).Take(dModel.dataTableParams.take).ToList();
            rResult.recordsFiltered = _signed.Count();
            List<ResourceVM> list = new List<ResourceVM>();
            ResourceVM model = new ResourceVM();

            foreach (var item in _signed)
            {
                model = new ResourceVM();
                model.ID = item.Id;
                model.LanguageID = item.LangId;
                model.KeyName = item.KeyName;
                model.Value = item.Value;
                model.LanguageName = item.Lang.Name;
                list.Add(model);
            }

            rResult.data = list;

            return rResult;
        }

        public IQueryable<Resource> GetAllAsIQueryable()
        => _repository.GetAsIQueryable();

    }
}
