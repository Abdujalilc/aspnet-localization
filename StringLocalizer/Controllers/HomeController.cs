using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace StringLocalizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer _localizer;
        public HomeController(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = _localizer["Header"];
            ViewData["Message"] = _localizer["Message"];
            return View();
        }
    }
}