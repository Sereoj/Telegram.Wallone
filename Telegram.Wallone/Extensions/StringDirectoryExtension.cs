using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Wallone.Extensions
{
    internal static class StringDirectoryExtension
    {
        /// <summary>Создание директории</summary>
        /// <param name="Path"></param>
        public static void CreateDirectory(this string Path)
        {
            Directory.CreateDirectory(Path);
        }

        /// <summary>Проверка директории</summary>
        /// <param name="Path"></param>
        public static bool ExistsDirectory(this string Path)
        {
            return Directory.Exists(Path);
        }

        /// <summary>Удаление директории</summary>
        /// <param name="Path"></param>
        public static void DeleteDirectory(this string Path)
        {
            Directory.Delete(Path, true);
        }
    }
}
