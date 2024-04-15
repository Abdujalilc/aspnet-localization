using Microsoft.AspNetCore.Mvc;
using JsLocalizeText.Models;
using JsLocalizeText.ViewModels;
using JsLocalizeText.Services;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JsLocalizeText.Controllers
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
        public IActionResult ChangeCulture(string culture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);            
            Response.Cookies.Append("culture", culture);
            return Redirect(returnUrl);
        }
    }
}
