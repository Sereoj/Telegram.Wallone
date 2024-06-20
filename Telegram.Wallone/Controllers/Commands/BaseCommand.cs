using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Wallone.Routes;

namespace Telegram.Wallone.Controllers.Commands
{
    internal class BaseCommand
    {
        internal static async Task<Message> Start(ITelegramBotClient telegramBot, Message message, CancellationToken cancellationToken)
        {
            var messages =
                $"Hello, {message?.From?.Username} 😀\n" +
                $"Select a language!";

            await telegramBot.SendChatActionAsync(
                    chatId: message.Chat.Id,
                    chatAction: ChatAction.Typing,
                    cancellationToken: cancellationToken);

            await Task.Delay(500, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                            InlineKeyboardButton.WithCallbackData(text: "Русский", callbackData: LangRoute.Russia),
                            InlineKeyboardButton.WithCallbackData(text: "English", callbackData: LangRoute.English),

                });

            return await telegramBot.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }

        internal static async Task<Message> Auth(ITelegramBotClient telegramBot, Message message, CancellationToken cancellationToken)
        {


            var messages = "Для того чтобы авторизоваться в Telegram боте, необходимо ввести идентификационный номер на нашем сайте.\nВаш код: CODE" +
                "Перейдите по ссылке ниже: https://link.app/acttount/telegram \n" +
                "И введите данный код.\n" +
                "Внимание, данный код необходимо ввести только в телеграмм боту, никому не передайте его.";

            await telegramBot.SendChatActionAsync(
                chatId: message.Chat.Id,
                chatAction: ChatAction.Typing,
                cancellationToken: cancellationToken);

            await Task.Delay(500, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = new(new[]
{
                            InlineKeyboardButton.WithCallbackData(text: "Проверить", callbackData: AuthRoute.User),

                });

            return await telegramBot.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }

        internal static async Task<Message> Account(ITelegramBotClient telegramBot, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        internal static async Task<Message> Events(ITelegramBotClient telegramBot, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        internal static async Task<Message> Lang(ITelegramBotClient telegramBot, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        internal static async Task<Message> Usage(ITelegramBotClient telegramBot, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
