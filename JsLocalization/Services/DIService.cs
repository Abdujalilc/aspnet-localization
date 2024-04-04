using JsLocalization.DAL;
using JsLocalization.Models;

namespace JsLocalization.Services
{
    public static class DIService
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDbLanguageResourcesService, DbLanguageResourcesService>();
            services.AddScoped<IDataTableInputParamsService, DataTableInputParamsService>();
            services.AddScoped<ISpLanguagesService, SpLanguagesService>();            
            services.AddScoped<IRepository<SpLanguage>, Repository<SpLanguage>>();
            services.AddScoped<IRepository<DbLanguageResource>, Repository<DbLanguageResource>>();
        }
    }
}
