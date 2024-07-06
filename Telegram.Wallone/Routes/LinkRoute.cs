using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Wallone.Routes
{
    /**
     * Знаю, что можно в json, пока так.
     */
    internal static class LinkRoute
    {
        public static string LinkBot { get; set; } = "@wallone_bot"; //Основной бот

        /**
         * Channels
         */
        public static string WalloneNews { get; set; } = "@wallone_news";
        public static string WalloneChannel { get; set; } = "@wallone_channel";
        public static string WalloneApp { get; set; } = "@walloneapp";

        /**
         * Groups
         */
        public static string VKWalloneGroup { get; set; } = "https://vk.com/wallone_group";

        public static string SiteLink { get; set; } = "https://wallone.app";
        public static string InviteLinkGroup { get; set; } = "https://t.me/+7tPe2NSr66AyNzM6";
    }
}
