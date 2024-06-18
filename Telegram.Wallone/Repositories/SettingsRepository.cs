using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Wallone.Interfaces;

namespace Telegram.Wallone.Repositories
{
    internal class SettingsRepository
    {
        protected static ISettings SettingsInstance { get; set; }

        public static void Set(ISettings settings)
        {
            if (settings != null)
            {
                SettingsInstance = settings;
            }
        }

        public static ISettings Get()
        {
            return SettingsInstance;
        }
    }
}
