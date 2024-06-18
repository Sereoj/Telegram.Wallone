using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Telegram.Wallone.Builders.Base;
using Telegram.Wallone.Interfaces;
using Telegram.Wallone.Models;
using Telegram.Wallone.Repositories;


namespace Telegram.Wallone.Builders
{
    internal class LogBuilder : BaseBuilder
    {
        public LogBuilder()
        {
        }

        public LogBuilder Create()
        {
            LogRepository.Set(new Log());
            return this;
        }
    }
}
