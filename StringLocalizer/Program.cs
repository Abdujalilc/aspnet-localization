using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using StringLocalizer;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IStringLocalizer, CustomStringLocalizer>();
builder.Services.AddControllersWithViews();
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
