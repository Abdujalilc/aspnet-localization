using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EFLocalizationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer _localizer;

        public HomeController(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }
        public string Test()
        {
            string message = _localizer["Message"];
            return message;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}