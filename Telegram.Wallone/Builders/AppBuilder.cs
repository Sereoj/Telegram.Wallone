using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Wallone.Builders.Base;
using Telegram.Wallone.Interfaces;
using Telegram.Wallone.Services.Loggings;

namespace Telegram.Wallone.Builders
{
    internal class AppBuilder : BaseBuilder
    {
        private IBuilder appSettings;

        public AppBuilder Query(IBuilder interfaces)
        {
            appSettings = interfaces;
            return this;
        }

        internal void Start()
        {
            ConsoleLogService.Send("Приложение запущено :D", Models.MessageType.Information, typeof(AppBuilder));
        }
    }
}
