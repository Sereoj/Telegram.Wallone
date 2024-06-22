using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Wallone.Routes;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Controllers.Commands
{
    public class BaseCommand
    {
        public static LocalizationService _localizationService { get; set; }
        public BaseCommand(LocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        internal async Task<Message> Account(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var messages = _localizationService.GetLocalizedString("greeting");

            InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Посты", callbackData: AuthRoute.User),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Посты", callbackData: AuthRoute.User),
                }
                });

            return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }

        internal async Task<Message> Auth(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var messages = _localizationService.GetLocalizedString("greeting");

            await botClient.SendChatActionAsync(
                chatId: message.Chat.Id,
                chatAction: ChatAction.Typing,
                cancellationToken: cancellationToken);

            await Task.Delay(500, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "Проверить", callbackData: AuthRoute.User),
            });

            return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }

        internal async Task<Message> Event(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        internal async Task<Message> Lang(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var messages = _localizationService.GetLocalizedString("select");

            await botClient.SendChatActionAsync(
                    chatId: message.Chat.Id,
                    chatAction: ChatAction.Typing,
                    cancellationToken: cancellationToken);

            await Task.Delay(500, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                            InlineKeyboardButton.WithCallbackData(text: "Русский", callbackData: LangRoute.Russia),
                            InlineKeyboardButton.WithCallbackData(text: "English", callbackData: LangRoute.English),

                });

            return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }

        internal async Task<Message> Start(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var messages =
                $"Hello, {message?.From?.Username} 😀\n" +
                $"Select a language!";

            await botClient.SendChatActionAsync(
                    chatId: message.Chat.Id,
                    chatAction: ChatAction.Typing,
                    cancellationToken: cancellationToken);

            await Task.Delay(500, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                            InlineKeyboardButton.WithCallbackData(text: "Русский", callbackData: LangRoute.Russia),
                            InlineKeyboardButton.WithCallbackData(text: "English", callbackData: LangRoute.English),

            });

            return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }

        internal async Task<Message> Usage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
