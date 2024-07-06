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
        private readonly LocalizationService _localizationService;
        private readonly LangHelper _langHelper;

        public CommandFactory(ITelegramBotClient botClient, LocalizationService localizationService, LangHelper langHelper)
        {
            _botClient = botClient;
            _localizationService = localizationService;
            _langHelper = langHelper;
        }

        public ICommand CreateCommand(string command)
        {
            return command switch
            {
                "/start" => new StartCommand(_botClient, _localizationService, _langHelper),
                "/auth" => new AuthorizationCommand(_botClient, _localizationService, _langHelper),
                "/subs" => new SubscribeCommand(_botClient, _localizationService, _langHelper),
                "/account" => new AccountCommand(_botClient, _localizationService, _langHelper),
                "/events" => new EventCommand(_botClient, _localizationService, _langHelper),
                "/lang" => new LangCommand(_botClient, _localizationService, _langHelper),
                _ => new UsageCommand(_botClient, _localizationService, _langHelper)
            };
        }
    }
}
