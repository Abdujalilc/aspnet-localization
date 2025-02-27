using EFLocalizationApp.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

public class CachingEFStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly LocalizationContext _context;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheOptions;

    public CachingEFStringLocalizerFactory(LocalizationContext context, IMemoryCache cache, MemoryCacheEntryOptions cacheOptions)
    {
        _context = context;
        _cache = cache;
        _cacheOptions = cacheOptions;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        // Check if the localized strings are already cached
        if (!_cache.TryGetValue("LocalizedStrings", out IDictionary<string, string> localizedStrings))
        {
            // If not cached, fetch them from the database and cache them
            localizedStrings = _context.Resources.ToDictionary(r => r.Key, r => r.Value);
            _cache.Set("LocalizedStrings", localizedStrings, _cacheOptions);
        }

        return new CachingEFStringLocalizer(localizedStrings);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        throw new NotImplementedException();
    }
}