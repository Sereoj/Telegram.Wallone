using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Wallone.Interfaces;

namespace Telegram.Wallone.Repositories
{
    internal class TelegramRepository
    {
        protected static TelegramBotClient TelegramInstance { get; set; }

        public static void Set(TelegramBotClient telegramInstance)
        {
            if (telegramInstance != null)
            {
                TelegramInstance = telegramInstance;
            }
        }

        public static TelegramBotClient Get()
        {
            return TelegramInstance;
        }
    }
}
