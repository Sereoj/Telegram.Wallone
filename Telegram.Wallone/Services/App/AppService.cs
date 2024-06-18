using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Wallone.Services.App
{
    internal class AppService
    {

        public static string GetLocalPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string GetPath(string path)
        {
            return Path.Combine(GetLocalPath() , path);
        }

        public static bool Exists(string path) => File.Exists(path);
        
    }
}
