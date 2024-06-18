using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Wallone.Services.Loggings;

namespace Telegram.Wallone.Exceptions
{
    internal class JsonException : Exception
    {
        public JsonException(string message)
        {
            LogService.Message(message, Models.MessageType.Error);
        }
    }
}
