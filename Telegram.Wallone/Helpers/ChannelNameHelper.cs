using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Wallone.Helpers
{
    public static class ChannelNameHelper
    {
        public static string GetValue(string channelName)
        {
            return channelName.StartsWith("@") ? channelName : "@" + channelName;
        }
    }
}
