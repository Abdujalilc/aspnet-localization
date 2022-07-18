using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LocalizationApp
{
    public class CustomStringLocalizer : IStringLocalizer
    {
        Dictionary<string, Dictionary<string, string>> resources;
        // ключи ресурсов
        const string HEADER = "Header";
        const string MESSAGE = "Message";

        public CustomStringLocalizer()
        {
            // словарь для английского языка
            Dictionary<string, string> enDict = new Dictionary<string, string>
            {
                {HEADER, "Welcome" },
                {MESSAGE, "Hello World!" }
            };
            // словарь для русского языка
            Dictionary<string, string> ruDict = new Dictionary<string, string>
            {
                {HEADER, "Добо пожаловать" },
                {MESSAGE, "Привет мир!" }
            };
            // словарь для немецкого языка
            Dictionary<string, string> deDict = new Dictionary<string, string>
            {
                {HEADER, "Willkommen" },
                {MESSAGE, "Hallo Welt!" }
            };
            // создаем словарь ресурсов
            resources = new Dictionary<string, Dictionary<string, string>>
            {
                {"en", enDict },
                {"ru", ruDict },
                {"de", deDict }
            };
        }
        // по ключу выбираем для текущей культуры нужный ресурс
        public LocalizedString this[string name]
        {
            get
            {
                var currentCulture = CultureInfo.CurrentUICulture;
                string val = "";
                if (resources.ContainsKey(currentCulture.Name))
                {
                    if (resources[currentCulture.Name].ContainsKey(name))
                    {
                        val = resources[currentCulture.Name][name];
                    }
                }
                return new LocalizedString(name, val);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return this;
        }
    }
}