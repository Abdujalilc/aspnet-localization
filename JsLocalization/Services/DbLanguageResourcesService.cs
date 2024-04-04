using JsLocalization.DAL;
using JsLocalization.Models;
using JsLocalization.ViewModels;
using System.Text.Json;

namespace JsLocalization.Services
{
    public interface IDbLanguageResourcesService
    {
        DbLanguageResource GetByID(int id);
        bool Create(DbLanguageResource role);
        int CreateRange(List<DbLanguageResource> model);
        bool Update(DbLanguageResource role);
        bool Delete(int id);
        string PublishLanguage();
        DataTableOutputParams<LanguageResourcesVM> LanguageResourcesList(SearchLanguageResourcesVM dModel);
        void PublishLanguageNew();
        IQueryable<DbLanguageResource> GetAllAsIQueryable();
        List<DbLanguageResource> GetByFormCollection(IFormCollection form);
    }
    public class DbLanguageResourcesService : IDbLanguageResourcesService
    {
        IRepository<DbLanguageResource> _repository;
        private readonly ISpLanguagesService _spLanguagesService;

        public DbLanguageResourcesService(IRepository<DbLanguageResource> repository, ISpLanguagesService spLanguagesService)
        {
            _repository = repository;
            _spLanguagesService = spLanguagesService;
        }

        public DbLanguageResource GetByID(int id)
        {
            var rep = _repository.Get(id);
            return rep;
        }

        public bool Create(DbLanguageResource model)
        {
            try
            {
                _repository.Insert(model);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(DbLanguageResource model)
        {
            try
            {
                var old = GetByID(model.Id);
                if (old != null)
                {
                    old.LangId = model.LangId;
                    old.Value = model.Value;
                    old.KeyName = model.KeyName;
                    _repository.Update(old);
                    return true;
                }
            }
            catch (Exception ex)
            { }
            return false;
        }

        public bool Delete(int id)
        {
            try
            {
                var fd = _repository.Get(id);
                _repository.Delete(fd);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public string PublishLanguage()
        {
            string folder = Path.GetFullPath("wwwroot/js/LanguageResourse/");
           
            var lan = GetAllAsIQueryable().Where(x => x.LangId == 1).Select(x => new { key = x.KeyName, value = x.Value }).ToList();
            var json = "LanguageDataUZ=" + JsonSerializer.Serialize(lan);
            string fileName = "LanguagesUZ.js";
            string fullPath = folder + fileName;
            File.WriteAllText(fullPath, json);

            lan = GetAllAsIQueryable().Where(x => x.LangId == 2).Select(x => new { key = x.KeyName, value = x.Value }).ToList();
            json = "LanguageDataRU=" + JsonSerializer.Serialize(lan);
            fileName = "LanguagesRU.js";
            fullPath = folder + fileName;
            File.WriteAllText(fullPath, json);

            lan = GetAllAsIQueryable().Where(x => x.LangId == 3).Select(x => new { key = x.KeyName, value = x.Value }).ToList();
            json = "LanguageDataEN=" + JsonSerializer.Serialize(lan);
            fileName = "LanguagesEN.js";
            fullPath = folder + fileName;
            File.WriteAllText(fullPath, json);
            File.WriteAllText(folder + "/LanguageToken.txt", Guid.NewGuid().ToString());
            return "";
        }
        public void PublishLanguageNew()
        {
            string folder = Path.GetFullPath("wwwroot/js/LanguageResourse/");
            var languageType = _spLanguagesService.GetAllAsIQueryable().Where(x => x.IsActive == true).ToList();
            var languageAll = GetAllAsIQueryable().Select(x => new { KeyName = x.KeyName, Value = x.Value, LangID = x.LangId }).ToList();
            string fileName = "";
            string fullPath = "";
            string jSONText = "var arrLang = {";
            foreach (var item in languageType)
            {
                var language = languageAll.Where(x => x.LangID == item.Id).ToList();
                jSONText = jSONText + "\"" + item.Code.ToLower() + "\":{";
                foreach (var model in language)
                {
                    jSONText = jSONText + "\"" + model.KeyName + "\":\"" + model.Value + "\",";
                }
                jSONText = jSONText.TrimEnd(',');
                jSONText = jSONText + "},";
            }
            jSONText = jSONText.TrimEnd(',');
            jSONText = jSONText + "}  ";
            
            fileName = "LanguageArray.js";
            fullPath = folder + fileName;
            File.WriteAllText(fullPath, jSONText);
            File.WriteAllText(folder + "/LanguageVersion.txt", Guid.NewGuid().ToString());
        }


        public DataTableOutputParams<LanguageResourcesVM> LanguageResourcesList(SearchLanguageResourcesVM dModel)
        {
            DataTableOutputParams<LanguageResourcesVM> rResult = new DataTableOutputParams<LanguageResourcesVM>();
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
            List<LanguageResourcesVM> list = new List<LanguageResourcesVM>();
            LanguageResourcesVM model = new LanguageResourcesVM();

            foreach (var item in _signed)
            {
                model = new LanguageResourcesVM();
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

        public IQueryable<DbLanguageResource> GetAllAsIQueryable()
        => _repository.GetAsIQueryable();

        public List<DbLanguageResource> GetByFormCollection(IFormCollection form)
        {
            List<DbLanguageResource> result = new List<DbLanguageResource>();
            var spLang = _spLanguagesService.GetAllAsIQueryable();
            var KeyName = form["KeyName"].FirstOrDefault();
            foreach (var item in spLang)
            {
                var one = new DbLanguageResource();
                one.KeyName = KeyName;
                one.LangId = item.Id;
                one.Value = form["Value_" + item.Code];
                result.Add(one);
            }
            return result;
        }

        public int CreateRange(List<DbLanguageResource> model)
        {
            return _repository.AddRange(model);
        }
    }
}
