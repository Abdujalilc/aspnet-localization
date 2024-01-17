using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

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
            string culture = CultureInfo.CurrentCulture.Name;
            return View("Index", culture);
        }
        public string GetCulture()
        {
            return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return RedirectToAction(nameof(Index));
        }
    }
}