using Microsoft.AspNetCore.Mvc;
using JsLocalization.Models;
using JsLocalization.ViewModels;
using JsLocalization.Services;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JsLocalization.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly ResourcesService _resourcesService;
        private readonly IDataTableInputParamsService _dataTableInputParamsService;
        private readonly ICultureService _cultureService;

        public ResourcesController(ResourcesService resourcesService, IDataTableInputParamsService dataTableInputParamsService, ICultureService cultureService)
        {
            _resourcesService = resourcesService;
            _dataTableInputParamsService = dataTableInputParamsService;
            _cultureService = cultureService;
        }
        public IActionResult Index()
        {
            ViewBag.LanguageList = _cultureService.GetAllAsIQueryable().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult ResourcesRead(IFormCollection form, SearchResourcesVM dModel)
        {
            dModel.dataTableParams = new DataTableInputParams();
            dModel.dataTableParams = _dataTableInputParamsService.ToModel(form);
            var rResult = _resourcesService.ResourcesList(dModel);
            return Json(new { recordsFiltered = rResult.recordsTotal, recordsTotal = rResult.recordsFiltered, data = rResult.data });
        }
        public IActionResult Create()
        {
            Resource model = new Resource();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(Resource model)
        {
            _resourcesService.Create(model);
            return RedirectToAction("Index", "Resources");
        }
        public IActionResult CreateRange()
        {            
            var model = _cultureService.GetAllAsIQueryable();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateRange(IFormCollection form)
        {
            var models=_resourcesService.GetByFormCollection(form);
            var insertedCount=_resourcesService.CreateRange(models);
            return RedirectToAction("Index", "Resources");
        }
        public IActionResult Edit(int id)
        {
            var entModel = _resourcesService.GetByID(id);
            if (entModel == null) return RedirectToAction("Index", "Resources");

            return View(entModel);
        }
        [HttpPost]
        public IActionResult Edit(Resource model)
        {
            _resourcesService.Update(model);
            return RedirectToAction("Index", "Resources");
        }
        public IActionResult PublishLanguage()
        {
            _resourcesService.PublishLanguageNew();
            return RedirectToAction("Index", "Resources");
        }
        public IActionResult ChangeCulture(string culture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);            
            Response.Cookies.Append("culture", culture);
            return Redirect(returnUrl);
        }
    }
}
