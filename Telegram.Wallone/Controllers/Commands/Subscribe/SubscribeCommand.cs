using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Wallone.Controllers.Commands.Authorization;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Routes;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Controllers.Commands.Subscribe
{
    internal class SubscribeCommand : Base.BaseCommand
    {
        private readonly ActionCommand Action;
        public SubscribeCommand(ITelegramBotClient botClient, LocalizationService localizationService, LangHelper langHelper) : base(botClient, localizationService, langHelper)
        {
            Action = new ActionCommand(botClient, localizationService, langHelper);
        }

        public override async Task<Message> ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            var messages = LangHelper.getLanguage("subs_group");

            await Action.ExecuteAsync(message, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithUrl(text:  LangHelper.getLanguage("subs_group.sub"), LinkRoute.InviteLinkGroup)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text:  LangHelper.getLanguage("subs_group.check"), callbackData: AccountRoute.SubsGroupCheck)
                }
            });

            return await BotClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messages,
                    replyMarkup: inlineKeyboard,
                    disableWebPagePreview: true,
                    parseMode: ParseMode.Markdown,
                    protectContent: true,
                    cancellationToken: cancellationToken);
        }
    }
}