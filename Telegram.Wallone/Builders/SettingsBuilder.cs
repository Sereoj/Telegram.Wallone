using Telegram.Wallone.Builders.Base;
using Telegram.Wallone.Interfaces;
using Telegram.Wallone.Models;
using Telegram.Wallone.Repositories;
using Telegram.Wallone.Services.App;
using Telegram.Wallone.Services.Settings;

namespace Telegram.Wallone.Builders
{
    internal class SettingsBuilder : BaseBuilder
    {
        public SettingsBuilder()
        {
        }

        public SettingsBuilder Create()
        {
            SettingsRepository.Set(new Settings()
            {
                IsLogging = true,
                Token = ""
            });
            return this;
        }

        public SettingsBuilder Get()
        {
            return this;
        }

        internal SettingsBuilder ExistNCreateDirectory(string v)
        {
            var pathDirectory = AppService.GetPath(v);
            if (!AppService.ExistDirectory(pathDirectory))
            {
                AppService.CreateDirectory(pathDirectory);
            }
            return this;
        }

        internal SettingsBuilder CreateOrUpdateFile(string v)
        {

            SettingsService.Filename = v;

            if (AppService.ExistsFile(AppService.GetPath(v)))
            {
                SettingsService.LoadSettingsFromFile();
            }
            else
            {
                SettingsService.SaveSettings(v, Create().GetModel());
            }
            
            return this;
        }

        internal ISettings GetModel()
        {
            return SettingsRepository.Get();
        }

        internal bool IsLoggingEnabled()
        {
            return GetModel().IsLogging == true;
        }
    }
}
