WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
WebApplication app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
        string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
app.Run();