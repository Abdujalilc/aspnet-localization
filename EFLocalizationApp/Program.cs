using EFLocalizationApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//string? con_string = "Data Source=AppData\\LocalizationDB.db";
//builder.Services.AddSqlite<LocalizationContext>(con_string);

string? con_string = "Data Source=ACHULIEV;Initial Catalog=LocalizationDB;User ID=sa;Password=P@ssw0rd;Trust Server Certificate=True;";
builder.Services.AddDbContext<LocalizationContext>(options =>
{
    options.UseSqlServer(con_string);
    options.EnableSensitiveDataLogging();
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddTransient<IStringLocalizer, EFStringLocalizer>();

builder.Services.AddSingleton<IStringLocalizerFactory>(provider =>
{
    using (var scope = provider.CreateScope())
    {
        var localizationContext = scope.ServiceProvider.GetRequiredService<LocalizationContext>();
        var memoryCache = provider.GetRequiredService<IMemoryCache>();
        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1));
        return new CachingEFStringLocalizerFactory(localizationContext, memoryCache, cacheOptions);
    }
});

builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    factory.Create(null);
})
.AddViewLocalization();


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
