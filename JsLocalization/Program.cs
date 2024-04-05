using Extensions;
using JsLocalization.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddServices();

string? con_string = "Data Source=AppData\\LocalizationDB.db";
builder.Services.AddSqlite<LocalizationContext>(con_string);
WebApplication app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();