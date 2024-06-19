using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Wallone.Repositories;

namespace Telegram.Wallone.Controllers
{
    internal class BaseTelegramController
    {
        public TelegramBotClient telegramBot { get; set; }
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
            Task.Run(() => HelloWorld());
            Console.ReadLine();
            tokenSource.Cancel();


        }

        public async Task HelloWorld()
        {
            var me = await telegramBot.GetMeAsync();
            Console.WriteLine($"Start listening for @{me.Username}");
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            // Echo received message text
            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You said:\n" + messageText,
                cancellationToken: cancellationToken);
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
