using Newtonsoft.Json;

public class LocalizationService
{
    private readonly Dictionary<string, Dictionary<string, string>> _localizationData;
    private string _language = "ru";

    public LocalizationService()
    {
        _localizationData = LoadLocalizationData();
    }

    private Dictionary<string, Dictionary<string, string>> LoadLocalizationData()
    {
        var localizationData = new Dictionary<string, Dictionary<string, string>>();
        var languageFiles = Directory.GetFiles("Localization", "*.json");

        foreach (var file in languageFiles)
        {
            var language = Path.GetFileNameWithoutExtension(file);
            var fileContent = File.ReadAllText(file);
            var translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(fileContent);
            localizationData[language] = translations;
        }

        return localizationData;
    }

    public void SetLanguage(string lang)
    {
        if (_localizationData.ContainsKey(lang))
        {
            _language = lang;
        }
    }

    public string GetLanguage()
    {
        return _language;
    }

    public string GetLocalizedString(string key)
    {
        if (_localizationData.TryGetValue(_language, out var languageData))
        {
            if (languageData.TryGetValue(key, out var localizedString))
            {
                return localizedString;
            }
        }
        return $"{key}";
    }
}