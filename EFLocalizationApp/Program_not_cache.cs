/*using EFLocalizationApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

string? con_string = "Data Source=AppData\\LocalizationDB.db";
builder.Services.AddSqlite<LocalizationContext>(con_string);

builder.Services.AddTransient<IStringLocalizer, EFStringLocalizer>();
//this factory initialize DB from ListObject
builder.Services.AddSingleton<IStringLocalizerFactory>(new EFStringLocalizerFactory(con_string));
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

app.Run();*/
