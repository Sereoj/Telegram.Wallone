using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Routes;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Controllers.Commands
{
    public class BaseCommand
    {
        private LangHelper _langHelper;
        public static LocalizationService _localizationService { get; set; }
        public BaseCommand(LocalizationService localizationService, LangHelper langHelper)
        {
            _langHelper = langHelper;
            _localizationService = localizationService;
        }

        protected InlineKeyboardMarkup getKeyboardButtonLanguage()
        {
            return new(new[]
            {
                InlineKeyboardButton.WithCallbackData(text: _langHelper.getLanguage("language.russia"), callbackData: LangRoute.Russia),
                InlineKeyboardButton.WithCallbackData(text:  _langHelper.getLanguage("language.english"), callbackData: LangRoute.English),
            });
        }
        internal async Task<Message> Subs(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var messages = _langHelper.getLanguage("subs_group");

            await sendChatActionAsync(botClient, message, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithUrl(text:  _langHelper.getLanguage("subs_group.sub"), LinkRoute.InviteLinkGroup)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text:  _langHelper.getLanguage("subs_group.check"), callbackData: AccountRoute.SubsGroupCheck)
                }
            });

            return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    protectContent: true,
                    cancellationToken: cancellationToken);
        }

        internal async Task<Message> Account(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var messages = _langHelper.getLanguage("account");

            InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text:  _langHelper.getLanguage("account.popular_images"), callbackData: AccountRoute.PopularImages),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text:  _langHelper.getLanguage("account.recently_purchased_images"), callbackData: AccountRoute.Balance),
                    }
                });

            return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    protectContent: true,
                    cancellationToken: cancellationToken);
        }

        internal async Task<Message> Auth(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var messages = _langHelper.getLanguage("auth");

            await sendChatActionAsync(botClient, message, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                InlineKeyboardButton.WithCallbackData(text:  _langHelper.getLanguage("auth.check"), callbackData: AuthRoute.User),
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
            var messages = _langHelper.getLanguage("language");

            await sendChatActionAsync(botClient, message, cancellationToken);
            InlineKeyboardMarkup inlineKeyboard = getKeyboardButtonLanguage();

            return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }

        internal async Task<Message> Start(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var messages = _langHelper.getLanguage("start");


            await sendChatActionAsync(botClient, message, cancellationToken);
            InlineKeyboardMarkup inlineKeyboard = getKeyboardButtonLanguage();

            return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }
        internal async Task<Message> AuthorizeUser(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var messages = "Авторизировались";

            return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }
        internal async Task<Message> Usage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var messages = "[@username](@username)";

            return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
        }

        private static async Task sendChatActionAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await botClient.SendChatActionAsync(
                    chatId: message.Chat.Id,
                    chatAction: ChatAction.Typing,
                    cancellationToken: cancellationToken);

            await Task.Delay(500, cancellationToken);
        }
    }
}
