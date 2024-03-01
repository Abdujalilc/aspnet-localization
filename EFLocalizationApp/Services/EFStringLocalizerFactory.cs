using EFLocalizationApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Services
{
    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        string _connectionString;
        public EFStringLocalizerFactory(string connection)
        {
            _connectionString = connection;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return CreateStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return CreateStringLocalizer();
        }

        private IStringLocalizer CreateStringLocalizer()
        {
            LocalizationContext _db = new LocalizationContext(new DbContextOptionsBuilder<LocalizationContext>().UseSqlServer(_connectionString).Options);
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