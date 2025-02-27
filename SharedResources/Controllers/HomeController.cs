using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SharedResources.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer;
        }

        public IActionResult Index()
        {
            var culture = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name;
            Console.WriteLine($"Current Culture: {culture}");

            // Debug output
            Console.WriteLine($"Localized Welcome: {_sharedLocalizer["Welcome"].Value}");

            string message = _sharedLocalizer["Welcome"].Value;
            return View("Index", message);
        }
    }
}
