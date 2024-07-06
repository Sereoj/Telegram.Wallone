using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Wallone.Controllers.Commands.Account;
using Telegram.Wallone.Helpers;

namespace Telegram.Wallone.Utilities
{
    public static class SubscriptionUtilities
    {

        public static async Task<bool> SubscriptionCheck(ITelegramBotClient botClient, Message? message, string channelName)
        {
            var groupId = await GroupUtilities.GetGroupIdAsync(botClient, ChannelNameHelper.GetValue(channelName));

            if (await GroupUtilities.IsGroupMember(botClient, groupId, message.Chat.Id))
            {
                return true;
            }
            return false;
        }
    }
}
