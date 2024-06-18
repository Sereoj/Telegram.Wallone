﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Wallone.Interfaces
{
    internal interface ISettings
    {
        public string? Token { get; set; }
        public bool? IsLogging { get; set; }
    }
}
