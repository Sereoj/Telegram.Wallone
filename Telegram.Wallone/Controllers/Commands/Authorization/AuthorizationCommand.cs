using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Routes;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Controllers.Commands.Authorization
{
    internal class AuthorizationCommand : Base.BaseCommand
    {
        private readonly ActionCommand Action;
        private readonly LocalizationService localizationService;

        public AuthorizationCommand(ITelegramBotClient botClient, LocalizationService localizationService, LangHelper langHelper) : base(botClient, localizationService, langHelper)
        {
            Action = new ActionCommand(botClient, localizationService, langHelper);
            this.localizationService = localizationService;
        }

        public override async Task<Message> ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            var messages = LangHelper.getLanguage("auth");

            await Action.ExecuteAsync(message, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                InlineKeyboardButton.WithCallbackData(text:  LangHelper.getLanguage("auth.check"), callbackData: AuthRoute.User),
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
            var messages = $"Авторизировались {localizationService.GetLanguage()}";

            return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }
    }
}