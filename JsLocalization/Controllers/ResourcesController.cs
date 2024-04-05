using Microsoft.AspNetCore.Mvc;
using JsLocalization.Models;
using JsLocalization.ViewModels;
using JsLocalization.Services;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JsLocalization.Controllers
{
    public class ResourceController : Controller
    {
        private readonly ResourceService _resourceService;
        private readonly IDataTableInputParamsService _dataTableInputParamsService;
        private readonly ICultureService _cultureService;

        public ResourceController(ResourceService resourceService, IDataTableInputParamsService dataTableInputParamsService, ICultureService cultureService)
        {
            _resourceService = resourceService;
            _dataTableInputParamsService = dataTableInputParamsService;
            _cultureService = cultureService;
        }
        public IActionResult Index()
        {
            ViewBag.LanguageList = _cultureService.GetAllAsIQueryable().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult ResourceRead(IFormCollection form, SearchResourceVM dModel)
        {
            dModel.dataTableParams = new DataTableInputParams();
            dModel.dataTableParams = _dataTableInputParamsService.ToModel(form);
            var rResult = _resourceService.ResourceList(dModel);
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
            _resourceService.Create(model);
            return RedirectToAction("Index", "Resource");
        }
        public IActionResult CreateRange()
        {            
            var model = _cultureService.GetAllAsIQueryable();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateRange(IFormCollection form)
        {
            var models=_resourceService.GetByFormCollection(form);
            var insertedCount=_resourceService.CreateRange(models);
            return RedirectToAction("Index", "Resource");
        }
        public IActionResult Edit(int id)
        {
            var entModel = _resourceService.GetByID(id);
            if (entModel == null) return RedirectToAction("Index", "Resource");

            return View(entModel);
        }
        [HttpPost]
        public IActionResult Edit(Resource model)
        {
            _resourceService.Update(model);
            return RedirectToAction("Index", "Resource");
        }
        public IActionResult UpdateResource()
        {
            _resourceService.UpdateResourceFIle();
            return RedirectToAction("Index", "Resource");
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
