using EFLocalizationApp;
using EFLocalizationApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

string connection = "Server=(localdb)\\mssqllocaldb;Database=localizationdb;Trusted_Connection=True;";
builder.Services.AddDbContext<LocalizationContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<IStringLocalizer, EFStringLocalizer>();
builder.Services.AddSingleton<IStringLocalizerFactory>(new EFStringLocalizerFactory(connection));
builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization(options => {
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    factory.Create(null);
})
.AddViewLocalization(); ;

var app = builder.Build();

var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru"),
                new CultureInfo("de")
            };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
