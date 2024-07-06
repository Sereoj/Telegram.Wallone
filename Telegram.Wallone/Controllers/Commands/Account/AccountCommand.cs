using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Routes;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Controllers.Commands.Account
{
    public class AccountCommand : Base.BaseCommand
    {
        public AccountCommand(ITelegramBotClient botClient, LocalizationService localizationService, LangHelper langHelper) : base(botClient, localizationService, langHelper)
        {
        }

        public override async Task<Message> ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            var messages = LangHelper.getLanguage("account");

            InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text:  LangHelper.getLanguage("account.popular_images"), callbackData: AccountRoute.PopularImages),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text:  LangHelper.getLanguage("account.recently_purchased_images"), callbackData: AccountRoute.Balance),
                    }
                });

            return await BotClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    protectContent: true,
                    disableWebPagePreview: true,
                    cancellationToken: cancellationToken);
        }
    }
}
