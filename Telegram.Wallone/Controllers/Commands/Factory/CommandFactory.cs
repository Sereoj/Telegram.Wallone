using Telegram.Bot;
using Telegram.Wallone.Controllers.Commands.Account;
using Telegram.Wallone.Controllers.Commands.Authorization;
using Telegram.Wallone.Controllers.Commands.Base;
using Telegram.Wallone.Controllers.Commands.Event;
using Telegram.Wallone.Controllers.Commands.Subscribe;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Controllers.Commands.Factory
{
    public class CommandFactory
    {
        private readonly ITelegramBotClient _botClient;
        private readonly LocalizationService localizationService;
        private readonly LangHelper langHelper;

        public CommandFactory(ITelegramBotClient botClient, LocalizationService localizationService, LangHelper langHelper)
        {
            _botClient = botClient;
            this.localizationService = localizationService;
            this.langHelper = langHelper;
        }

        public ICommand CreateCommand(string command)
        {
            return command switch
            {
                "/start" => new StartCommand(_botClient, localizationService, langHelper),
                "/auth" => new AuthorizationCommand(_botClient, localizationService, langHelper),
                "/subs" => new SubscribeCommand(_botClient, localizationService, langHelper),
                "/account" => new AccountCommand(_botClient, localizationService, langHelper),
                "/events" => new EventCommand(_botClient, localizationService, langHelper),
                "/lang" => new LangCommand(_botClient, localizationService, langHelper),
                _ => new UsageCommand(_botClient, localizationService, langHelper)
            };
        }
    }
}
