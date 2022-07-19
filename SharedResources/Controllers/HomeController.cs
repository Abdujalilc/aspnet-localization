using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SharedResources.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public HomeController(IStringLocalizer<HomeController> localizer,
                   IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<IActionResult> Index()
        {
            // получаем ресурс Message
            Object message = _sharedLocalizer["Message"];
            return View(message);
        }
    }
}