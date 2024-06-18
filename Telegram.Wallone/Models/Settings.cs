using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Wallone.Interfaces;

namespace Telegram.Wallone.Models
{
    internal class Settings : ISettings
    {
        public string? Token { get; set; }
        public bool? IsLogging { get; set; }
    }
}
