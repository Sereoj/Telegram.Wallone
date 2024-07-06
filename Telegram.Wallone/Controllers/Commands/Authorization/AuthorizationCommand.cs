using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Wallone.Controllers.Commands.Base;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Routes;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Controllers.Commands.Authorization
{
    public class AuthorizationCommand : BaseCommand
    {
        private readonly LocalizationService _localizationService;

        public AuthorizationCommand(ITelegramBotClient botClient, LocalizationService localizationService, LangHelper langHelper)
            : base(botClient, localizationService, langHelper)
        {
            _localizationService = localizationService;
        }

        public override async Task<Message> ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            var messages = LangHelper.getLanguage("auth");

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
            InlineKeyboardButton.WithCallbackData(text: LangHelper.getLanguage("auth.check"), callbackData: AuthRoute.User),
        });

            return await BotClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: messages,
                replyMarkup: inlineKeyboard,
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken);
        }

        public async Task<Message> AuthorizeMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var messages = $"Авторизировались {_localizationService.GetLanguage()}";

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: messages,
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken);
        }
    }
}