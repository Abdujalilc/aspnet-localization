using EFLocalizationApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EFLocalizationApp
{
    public class EFStringLocalizer : Microsoft.Extensions.Localization.IStringLocalizer
    {
        private readonly LocalizationContext _db;

        public EFStringLocalizer(LocalizationContext db)
        {
            _db = db;
        }

        public Microsoft.Extensions.Localization.LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new Microsoft.Extensions.Localization.LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public Microsoft.Extensions.Localization.LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new Microsoft.Extensions.Localization.LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        public Microsoft.Extensions.Localization.IStringLocalizer WithCulture(CultureInfo culture)
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            return new EFStringLocalizer(_db);
        }

        public IEnumerable<Microsoft.Extensions.Localization.LocalizedString> GetAllStrings(bool includeAncestorCultures)
        {
            return _db.Resources
                .Include(r => r.Culture)
                .Where(r => r.Culture.Name == CultureInfo.CurrentCulture.Name)
                .Select(r => new Microsoft.Extensions.Localization.LocalizedString(r.Key, r.Value));
        }

        private string GetString(string name)
        {
            return _db.Resources
                .Include(r => r.Culture)
                .Where(r => r.Culture.Name == CultureInfo.CurrentCulture.Name)
                .FirstOrDefault(r => r.Key == name)?.Value;
        }
    }
}