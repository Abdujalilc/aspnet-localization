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
            foreach (LocalizedString item in _sharedLocalizer.GetAllStrings())
                Console.WriteLine($"Key: {item.Name}, Value: {item.Value}");

            string? culture = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name;
            Console.WriteLine($"Current Culture: {culture}");

            Console.WriteLine($"Localized Welcome: {_sharedLocalizer["Welcome"].Value}");

            string message = _sharedLocalizer["Welcome"].Value;
            return View("Index", message);
        }
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }
        /*
            <a href="/Home/SetLanguage?culture=en">English</a> |
            <a href="/Home/SetLanguage?culture=ru">Русский</a> |
            <a href="/Home/SetLanguage?culture=de">Deutsch</a>
         */
    }
}
