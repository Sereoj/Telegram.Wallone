using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Wallone.Interfaces;

namespace Telegram.Wallone.Repositories
{
    internal class LogRepository
    {
        private static ILog LogInstance { get; set; }

        public static void Set(ILog log)
        {
            if (log != null)
            {
                LogInstance = log;
            }
        }

        public static ILog Get()
        {
            return LogInstance;
        }
    }
}
