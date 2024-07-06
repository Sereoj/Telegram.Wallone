using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Controllers.Commands.Base
{
    public abstract class BaseCommand : ICommand
    {
        protected readonly ITelegramBotClient BotClient;
        protected readonly LocalizationService LocalizationService;
        protected readonly LangHelper LangHelper;

        public BaseCommand(ITelegramBotClient botClient, LocalizationService localizationService, LangHelper langHelper)
        {
            BotClient = botClient;
            LocalizationService = localizationService;
            LangHelper = langHelper;
        }

        public abstract Task<Message> ExecuteAsync(Message message, CancellationToken cancellationToken);
    }
}
