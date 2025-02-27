using Microsoft.Extensions.Localization;

public class CachingEFStringLocalizer : IStringLocalizer
{
    private readonly IDictionary<string, string> _localizedStrings;

    public CachingEFStringLocalizer(IDictionary<string, string> localizedStrings)
    {
        _localizedStrings = localizedStrings ?? throw new ArgumentNullException(nameof(localizedStrings));
    }

    public LocalizedString this[string name] => GetLocalizedString(name);

    public LocalizedString this[string name, params object[] arguments] => GetLocalizedString(name);

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        throw new NotImplementedException();
    }

    private LocalizedString GetLocalizedString(string name)
    {
        if (_localizedStrings.TryGetValue(name, out string value))
        {
            return new LocalizedString(name, value);
        }
        return new LocalizedString(name, name);
    }
}