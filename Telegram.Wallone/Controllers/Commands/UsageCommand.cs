using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Controllers.Commands
{
    internal class UsageCommand : Base.BaseCommand
    {
        public UsageCommand(ITelegramBotClient botClient, LocalizationService localizationService, LangHelper langHelper) : base(botClient, localizationService, langHelper)
        {
        }

        public override async Task<Message> ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            var messages = "[@username](@username)";

            return await BotClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }
    }
}