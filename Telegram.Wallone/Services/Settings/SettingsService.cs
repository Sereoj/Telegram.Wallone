using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Wallone.Builders;
using Telegram.Wallone.Exceptions;
using Telegram.Wallone.Interfaces;
using Telegram.Wallone.Models;

namespace Telegram.Wallone.Services.Settings
{
    internal static class SettingsService
    {
        public static ISettings SettingsInstance { get; set; }

        public static void LoadSettings(ISettings settings)
        {
            if(settings != null)
            {
                SettingsInstance = settings;
            }
            SettingsInstance = new SettingsBuilder(new Settings());
            throw new SettingsException("Settings not loaded");
        }
        
    }
}
