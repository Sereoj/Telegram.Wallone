using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Wallone.Extensions;

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

        public static bool ExistsFile(string path)
        {
            return path.ExistsFile();
        }

        public static void CreateDirectory(string path)
        {
            path.CreateDirectory();
        }

        public static void RemoveDirectory(string path)
        {
            path.DeleteDirectory();
        }

        public static bool ExistDirectory(string path)
        {
            return path.ExistsDirectory();
        }
    }
}
