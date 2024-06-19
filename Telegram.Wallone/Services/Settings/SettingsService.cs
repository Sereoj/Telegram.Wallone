using Newtonsoft.Json;
using Telegram.Wallone.Builders;
using Telegram.Wallone.Exceptions;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Interfaces;
using Telegram.Wallone.Models;
using Telegram.Wallone.Repositories;
using Telegram.Wallone.Services.App;
using Telegram.Wallone.Services.Loggings;
using JsonException = Telegram.Wallone.Exceptions.JsonException;

namespace Telegram.Wallone.Services.Settings
{
    internal static class SettingsService
    {
        private static ISettings? SettingsInstance { get; set; }
        public static string Filename { get; set; } = "Settings/App.json";

        public static void LoadSettingsFromFile()
        {
            if (SettingsInstance == null)
            {
                try
                {
                    var path = AppService.GetPath(Filename);
                    
                    var jsonText = File.ReadAllText(path);
                    if (JsonHelper.IsValidJson(jsonText))
                    {
                       LoadSettings(Json<Models.Settings>.Decode(jsonText));
                    }
                }
                catch (Exception ex)
                {
                    throw new JsonException(ex.Message);
                }
            }
        }
        public static void LoadSettings(ISettings settings)
        {

            //Загружаем настройки
            if(settings != null)
            {
                SettingsRepository.Set(settings);
                ConsoleLogService.Send("Настройки загружены", Models.MessageType.Information, typeof(ISettings));
            }
            else
            {
                //Если не удалось, то выводим ошибку. Запуск программы прекращаем.
                throw new SettingsException("Settings not loaded");
            }
        }

        public static ISettings GetSettings() => SettingsRepository.Get();

        public static string GetToken()
        {
            if(GetSettings().Token == null)
            {
                throw new SettingsException("Token not loaded");
            }
            return GetSettings().Token;
        }

        public static void SaveSettings(string file, ISettings settings)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
        
    }
}
