using EFLocalizationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFLocalizationApp
{
    public class EFStringLocalizerFactory : Microsoft.Extensions.Localization.IStringLocalizerFactory
    {
        string _connectionString;
        public EFStringLocalizerFactory(string connection)
        {
            _connectionString = connection;
        }

        public Microsoft.Extensions.Localization.IStringLocalizer Create(Type resourceSource)
        {
            return CreateStringLocalizer();
        }

        public Microsoft.Extensions.Localization.IStringLocalizer Create(string baseName, string location)
        {
            return CreateStringLocalizer();
        }

        private Microsoft.Extensions.Localization.IStringLocalizer CreateStringLocalizer()
        {
            LocalizationContext _db = new LocalizationContext( new DbContextOptionsBuilder<LocalizationContext>().UseSqlite(_connectionString).Options);
            // seed DB
            if (!_db.Cultures.Any())
            {
                _db.AddRange(
                    new Culture
                    {
                        Name = "en",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Hello", Value = "Hello" },
                            new Resource { Key = "Welcome", Value = "Welcome" }
                        }
                    },
                    new Culture
                    {
                        Name = "ru",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Hello", Value = "Привет" },
                            new Resource { Key = "Welcome", Value = "Добро пожаловать" }
                        }
                    },
                    new Culture
                    {
                        Name = "de",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Hello", Value = "Hallo" },
                            new Resource { Key = "Welcome", Value = "Willkommen" }
                        }
                    }
                );
                _db.SaveChanges();
            }
            return new EFStringLocalizer(_db);
        }
    }
}