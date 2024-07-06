using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Telegram.Wallone.Utilities
{
    public static class GroupUtilities
    {
        /// Получаем id группы
        public static async Task<long> GetGroupIdAsync(ITelegramBotClient botClient, string chatIdentifier)
        {
            var chat = await botClient.GetChatAsync(chatIdentifier);
            return chat.Id;
        }

        /// Проверяет находится ли пользователь в группе.
        public static async Task<bool> IsGroupMember(ITelegramBotClient botClient, long groupId, long userId)
        {
            var data = await botClient.GetChatMemberAsync(groupId, userId);
            return data.Status == ChatMemberStatus.Member ||
                    data.Status == ChatMemberStatus.Administrator ||
                    data.Status == ChatMemberStatus.Creator;
        }

        /// Проверяет является ли администратором группы.
        public static async Task<bool> IsGroupAdmin(ITelegramBotClient botClient, long groupId, long userId)
        {
            var data = await botClient.GetChatMemberAsync(groupId, userId);
            return data.Status == ChatMemberStatus.Administrator;
        }

        /// Проверяет является ли создателем группы.
        public static async Task<bool> IsGroupCreator(ITelegramBotClient botClient, long groupId, long userId)
        {
            var data = await botClient.GetChatMemberAsync(groupId, userId);
            return data.Status == ChatMemberStatus.Creator;
        }
    }
}
