using JsLocalization.DAL;
using JsLocalization.Models;
using JsLocalization.Services;

namespace Extensions
{
    public static class DependanceInjectionService
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ResourceService, ResourceService>();
            services.AddScoped<IDataTableInputParamsService, DataTableInputParamsService>();
            services.AddScoped<ICultureService, CultureService>();
            services.AddScoped<IRepository<Culture>, Repository<Culture>>();
            services.AddScoped<IRepository<Resource>, Repository<Resource>>();
        }
    }
}
