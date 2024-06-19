using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Wallone.Builders;
using Telegram.Wallone.Models;

namespace Telegram.Wallone.Services.Loggings
{
    internal class ConsoleLogService
    {
        public static void Send(string message, MessageType type, object useClass)
        {

            Console.WriteLine(Format(message, type, useClass));

            if (LogService.IsEnabled())
            {
                LogService.Message(message, type);  
            }

        }

        protected static string Format(string message, MessageType type, object useClass)
        {
            return $"{DateTime.Now} - {type} - {useClass} - {message}";
        }
    }
}
