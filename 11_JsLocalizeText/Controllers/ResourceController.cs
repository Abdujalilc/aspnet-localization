using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace JsLocalizeText.Controllers
{
    public class ResourceController : Controller
    {
        public IActionResult Index()
        {            
            return View();
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
