namespace Telegram.Wallone.Services
{
    public class LocalizationService
    {
        private readonly Dictionary<string, Dictionary<string, string>> _localizationData;
        private string _language = "en";

        public LocalizationService()
        {
            _localizationData = new Dictionary<string, Dictionary<string, string>>
            {
                ["en"] = new Dictionary<string, string>
                {
                    ["greeting"] = "Hello!",
                    ["choice"] = "Your choice has been received!",
                    ["select"] = "Select a language!"
                },
                ["ru"] = new Dictionary<string, string>
                {
                    ["greeting"] = "Привет!",
                    ["choice"] = "Ваш выбор получен!",
                    ["select"] = "Выберите язык!"
                }
            };
        }


        public void setLocale(string lang)
        {
            _language = lang;
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

            return $"[{key}]";
        }
    }
}
