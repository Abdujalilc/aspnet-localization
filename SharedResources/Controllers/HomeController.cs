using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Resources;
using System.Globalization;

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
            CultureInfo.CurrentCulture = new CultureInfo("ru");
            CultureInfo.CurrentUICulture = new CultureInfo("ru");

            foreach (var item in _sharedLocalizer.GetAllStrings())
            {
                Console.WriteLine($"Key: {item.Name}, Value: {item.Value}");
            }

            var culture = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name;
            Console.WriteLine($"Current Culture: {culture}");

            // Debug output
            Console.WriteLine($"Localized Welcome: {_sharedLocalizer["Welcome"].Value}");

            string message = _sharedLocalizer["Welcome"].Value;
            return View("Index", message);
        }
    }
}
