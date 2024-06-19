using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Wallone.Builders;
using Telegram.Wallone.Interfaces;
using Telegram.Wallone.Models;
using Telegram.Wallone.Repositories;
using Telegram.Wallone.Services.App;

namespace Telegram.Wallone.Services.Loggings
{

    internal class LogService
    {
        public static void Message(string message, MessageType messageType)
        {
            var path = AppService.GetPath(DefaultFileName());
            
            if(IsEnabled())
            {
                using (var file = new StreamWriter(path, true))
                {
                    file.WriteLineAsync(ContentFormatter(message, messageType));
                    file.Close();
                }
            }
        }


        public static bool IsEnabled()
        {
            return new SettingsBuilder()
                    .Get()
                    .IsLoggingEnabled();
        }
        public static string DefaultFileName()
        {
            return $"app-{DateTime.Now:yy-MM-dd}.log";
        }

        private static string ContentFormatter(string message, MessageType messageType)
        {
            return $"{DateTime.Now} - {messageType} - {message}";
        }
    }
}
