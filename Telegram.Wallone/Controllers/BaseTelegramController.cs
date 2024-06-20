using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Wallone.Controllers.Commands;
using Telegram.Wallone.Routes;
using Telegram.Wallone.Services.Loggings;

namespace Telegram.Wallone.Controllers
{
    internal class BaseTelegramController
    {
        public ITelegramBotClient telegramBot { get; set; }
        protected CancellationTokenSource tokenSource { get; set; } = new CancellationTokenSource();

        public BaseTelegramController(TelegramBotClient telegramBotClient)
        {
            telegramBot = telegramBotClient;

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            telegramBot.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: tokenSource.Token
            );
            Console.ReadLine();
            tokenSource.Cancel();
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {

                var handler = update switch
                {
                    // UpdateType.Unknown:
                    // UpdateType.ChannelPost:
                    // UpdateType.EditedChannelPost:
                    // UpdateType.ShippingQuery:
                    // UpdateType.PreCheckoutQuery:
                    // UpdateType.Poll:
                    { Message: { } message } => BotOnMessageReceived(message, cancellationToken),
                    { EditedMessage: { } message } => BotOnMessageReceived(message, cancellationToken),
                    { CallbackQuery: { } callbackQuery } => HandleCallbackQueryReceived(callbackQuery, cancellationToken),
                    _ => UnknownUpdateHandlerAsync(update, cancellationToken)
                };


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while handling {update.Type}: {ex}");
            }
        }

        private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
        {
            if (message.Text is not { } messageText)
                return;

            var action = messageText.Split(' ')[0] switch
            {
                "/start" => BaseCommand.Start(telegramBot, message, cancellationToken),
                "/auth" => BaseCommand.Auth(telegramBot, message, cancellationToken),
                "/account" => BaseCommand.Account(telegramBot, message, cancellationToken),
                "/events" => BaseCommand.Events(telegramBot, message, cancellationToken),
                "/lang" => BaseCommand.Lang(telegramBot, message, cancellationToken),
                _ => BaseCommand.Usage(telegramBot, message, cancellationToken)
            };

            Message sentMessage = await action;
            ConsoleLogService.Send($"Пользователь: {message?.From?.Username} написал сообщение в {message?.Chat.Id}:{sentMessage.MessageId}", Models.MessageType.Information, this);
        }

        private async Task HandleCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {

            switch (callbackQuery.Data)
            {
                case LangRoute.Russia:
                    await telegramBot.AnswerCallbackQueryAsync(callbackQuery.Id, "Ваш выбор получен! Russia");
                    break;
                case LangRoute.English:
                    await telegramBot.AnswerCallbackQueryAsync(callbackQuery.Id, $"Ваш выбор получен! English");
                    break;
                case AuthRoute.User:
                    await telegramBot.AnswerCallbackQueryAsync(callbackQuery.Id, $"Пользователь авторизирован: User");
                    break;
            }
        }
        private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }


    }
}
