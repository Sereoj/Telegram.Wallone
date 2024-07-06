using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Services;
using Telegram.Bot.Types.Enums;

namespace Telegram.Wallone.Controllers.Commands
{
    internal class ActionCommand : Base.BaseCommand
    {
        public ActionCommand(ITelegramBotClient botClient, LocalizationService localizationService, LangHelper langHelper) : base(botClient, localizationService, langHelper)
        {
        }

        public async override Task<Message> ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            await BotClient.SendChatActionAsync(
                    chatId: message.Chat.Id,
                    chatAction: ChatAction.Typing,
                    cancellationToken: cancellationToken);

            await Task.Delay(500, cancellationToken);

            return await Task.FromResult<Message>(null);
        }
    }
}