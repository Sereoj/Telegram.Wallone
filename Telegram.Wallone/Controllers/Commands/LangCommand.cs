using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Wallone.Controllers.Commands.Event;
using Telegram.Wallone.Controllers.Keyboards;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Controllers.Commands
{
    internal class LangCommand : Base.BaseCommand
    {
        protected readonly LanguageKeyboardButton LanguageKeyboardButton;
        private readonly ActionCommand Action;

        public LangCommand(ITelegramBotClient botClient, LocalizationService localizationService, LangHelper langHelper) : base(botClient, localizationService, langHelper)
        {
            LanguageKeyboardButton = new LanguageKeyboardButton(langHelper);
            Action = new ActionCommand(botClient, localizationService, langHelper);
        }

        public override async Task<Message> ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            var messages = LangHelper.getLanguage("language");

            await Action.ExecuteAsync(message, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = LanguageKeyboardButton.GetInlineKeyboardMarkup();

            return await BotClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }
    }
}