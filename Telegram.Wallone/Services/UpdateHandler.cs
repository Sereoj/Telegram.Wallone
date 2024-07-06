using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Wallone.Controllers.Commands.Account;
using Telegram.Wallone.Controllers.Commands.Authorization;
using Telegram.Wallone.Controllers.Commands.Base;
using Telegram.Wallone.Controllers.Commands.Factory;
using Telegram.Wallone.Controllers.Commands.Subscribe;
using Telegram.Wallone.Routes;
using Telegram.Wallone.Utilities;
using Telegram.Wallone.Validators;

namespace Telegram.Wallone.Services
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<UpdateHandler> _logger;
        private readonly CommandFactory _commandFactory;
        private readonly LocalizationService localizationService;
        private readonly AuthorizationCommand _authorizationCommand;
        private readonly AccountCommand _accountCommand;
        private readonly SubscribeCommand _subsCommand;

        public UpdateHandler(ITelegramBotClient botClient, ILogger<UpdateHandler> logger, CommandFactory commandFactory, LocalizationService localizationService)
        {
            _botClient = botClient;
            _logger = logger;
            _commandFactory = commandFactory;
            this.localizationService = localizationService;
            _authorizationCommand = (AuthorizationCommand)_commandFactory.CreateCommand("/auth");
            _accountCommand = (AccountCommand)_commandFactory.CreateCommand("/account");
            _subsCommand = (SubscribeCommand)_commandFactory.CreateCommand("/subs");
        }

        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _logger.LogInformation("HandleError: {ErrorMessage}", ErrorMessage);

            // Cooldown in case of network connection error
            if (exception is RequestException)
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }


        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update switch
            {
                { Message: { } message } => BotOnMessageReceived(message, cancellationToken),
                { EditedMessage: { } message } => BotOnMessageReceived(message, cancellationToken),
                { CallbackQuery: { } callbackQuery } => BotOnCallbackQueryReceived(callbackQuery, cancellationToken),
                { InlineQuery: { } inlineQuery } => BotOnInlineQueryReceived(inlineQuery, cancellationToken),
                { ChosenInlineResult: { } chosenInlineResult } => BotOnChosenInlineResultReceived(chosenInlineResult, cancellationToken),
                _ => UnknownUpdateHandlerAsync(update, cancellationToken)
            };

            await handler;
        }

        private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Тип полученного сообщения: {0}", message.Type);
            if (message.Text is not { } messageText)
                return;

            try
            {
                if (TokenValidator.Validation(messageText))
                {
                    await _authorizationCommand.AuthorizeMessage(_botClient, message, cancellationToken);
                    return;
                }

                var commandKey = messageText.Split(' ')[0];
                ICommand command = _commandFactory.CreateCommand(commandKey);

                Message sentMessage = await command.ExecuteAsync(message, cancellationToken);
                _logger.LogInformation($"Пользователь: {message?.From?.Username} написал сообщение в {message?.Chat.Id}:{sentMessage.MessageId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message: {MessageText}", messageText);
                // Обработка исключения или уведомление пользователя
            }
        }


        private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            var message = callbackQuery.Message;

            switch (callbackQuery.Data)
            {
                case LangRoute.Russia:
                    localizationService.SetLanguage("ru");
                    await AuthCommand(message, cancellationToken);
                    break;
                case LangRoute.English:
                    localizationService.SetLanguage("en");
                    await AuthCommand(message, cancellationToken);
                    break;
                case AuthRoute.User:
                    await _subsCommand.ExecuteAsync(message, cancellationToken);
                    await _botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId, cancellationToken);
                    break;
                case AccountRoute.SubsGroupCheck:
                    if(await SubscriptionUtilities.SubscriptionCheck(_botClient, message, "@walloneapp"))
                    {
                        await _accountCommand.ExecuteAsync(message, cancellationToken);
                        await _botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId, cancellationToken);
                    }
                    break;
                case AccountRoute.PopularImages:
                    break;
                case AccountRoute.RecentlyPurchasedImages:
                    break;
                case AccountRoute.Balance:
                    break;
                default:
                    await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"Пофиг мне на говнокод");
                    break;
            }
        }

        private async Task AuthCommand(Message? message, CancellationToken cancellationToken)
        {
            await _authorizationCommand.ExecuteAsync(message, cancellationToken);
            await _botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId, cancellationToken);
        }

        private async Task BotOnInlineQueryReceived(InlineQuery inlineQuery, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received inline query from: {InlineQueryFromId}", inlineQuery.From.Id);

            InlineQueryResult[] results = {
            // displayed result
            new InlineQueryResultArticle(
                id: "1",
                title: "TgBots",
                inputMessageContent: new InputTextMessageContent("hello"))
        };

            await _botClient.AnswerInlineQueryAsync(
                inlineQueryId: inlineQuery.Id,
                results: results,
                cacheTime: 0,
                isPersonal: true,
                cancellationToken: cancellationToken);
        }

        private async Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received inline result: {ChosenInlineResultId}", chosenInlineResult.ResultId);

            await Task.CompletedTask;
        }

        private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Неизвестный тип обновления: {UpdateType}", update.Type);
            return Task.CompletedTask;
        }
    }
}
