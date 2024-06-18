using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Wallone.Interfaces;

namespace Telegram.Wallone.Models
{
    enum MessageType
    {
        Error,
        Information,
        Warning
    }
    internal class Log : ILog
    {
        public string? Message { get; set; }
        public MessageType MessageType { get; set; }
    }
}
