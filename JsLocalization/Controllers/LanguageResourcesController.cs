using Microsoft.AspNetCore.Mvc;
using JsLocalization.Models;
using JsLocalization.ViewModels;
using JsLocalization.Services;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JsLocalization.Controllers
{
    public class LanguageResourcesController : Controller
    {
        private readonly IDbLanguageResourcesService _dbLanguageResourcesService;
        private readonly IDataTableInputParamsService _dataTableInputParamsService;
        private readonly ISpLanguagesService _spLanguagesService;

        public LanguageResourcesController(IDbLanguageResourcesService dbLanguageResourcesService, IDataTableInputParamsService dataTableInputParamsService, ISpLanguagesService spLanguagesService)
        {
            _dbLanguageResourcesService = dbLanguageResourcesService;
            _dataTableInputParamsService = dataTableInputParamsService;
            _spLanguagesService = spLanguagesService;
        }
        public IActionResult Index()
        {
            ViewBag.LanguageList = _spLanguagesService.GetAllAsIQueryable().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult LanguageResourcesRead(IFormCollection form, SearchLanguageResourcesVM dModel)
        {
            dModel.dataTableParams = new DataTableInputParams();
            dModel.dataTableParams = _dataTableInputParamsService.ToModel(form);
            var rResult = _dbLanguageResourcesService.LanguageResourcesList(dModel);
            return Json(new { recordsFiltered = rResult.recordsTotal, recordsTotal = rResult.recordsFiltered, data = rResult.data });
        }
        public IActionResult Create()
        {
            DbLanguageResource model = new DbLanguageResource();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(DbLanguageResource model)
        {
            _dbLanguageResourcesService.Create(model);
            return RedirectToAction("Index", "LanguageResources");
        }
        public IActionResult CreateRange()
        {            
            var model = _spLanguagesService.GetAllAsIQueryable();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateRange(IFormCollection form)
        {
            var models=_dbLanguageResourcesService.GetByFormCollection(form);
            var insertedCount=_dbLanguageResourcesService.CreateRange(models);
            return RedirectToAction("Index", "LanguageResources");
        }
        public IActionResult Edit(int id)
        {
            var entModel = _dbLanguageResourcesService.GetByID(id);
            if (entModel == null) return RedirectToAction("Index", "LanguageResources");

            return View(entModel);
        }
        [HttpPost]
        public IActionResult Edit(DbLanguageResource model)
        {
            _dbLanguageResourcesService.Update(model);
            return RedirectToAction("Index", "LanguageResources");
        }
        public IActionResult PublishLanguage()
        {
            _dbLanguageResourcesService.PublishLanguageNew();
            return RedirectToAction("Index", "LanguageResources");
        }
        public IActionResult ChangeCulture(string lang, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            var cookie = Request.Cookies["language"];
            cookie = lang;
            Response.Cookies.Append("language", cookie);
            return Redirect(returnUrl);
        }
    }
}
