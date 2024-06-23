using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Wallone.Controllers.Commands;
using Telegram.Wallone.Routes;

namespace Telegram.Wallone.Services
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<UpdateHandler> _logger;
        private readonly LocalizationService _localizationService;
        private readonly BaseCommand _baseCommand;

        public UpdateHandler(ITelegramBotClient botClient, ILogger<UpdateHandler> logger, LocalizationService localizationService, BaseCommand baseCommand)
        {
            _botClient = botClient;
            _logger = logger;
            _localizationService = localizationService;
            _baseCommand = baseCommand;
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
            _logger.LogInformation("Тип полученного сообщения: {MessageType}", message.Type);
            if (message.Text is not { } messageText)
                return;

            if (messageText.Contains("/auth") && messageText.Contains(":"))
            {
                await _baseCommand.AuthorizeUser(_botClient, message, cancellationToken);
                return;
            }

            var action = messageText.Split(' ')[0] switch
            {
                "/start" => _baseCommand.Start(_botClient, message, cancellationToken),
                "/auth" => _baseCommand.Auth(_botClient, message, cancellationToken),
                "/account" => _baseCommand.Account(_botClient, message, cancellationToken),
                "/events" => _baseCommand.Event(_botClient, message, cancellationToken),
                "/lang" => _baseCommand.Lang(_botClient, message, cancellationToken),
                _ => _baseCommand.Usage(_botClient, message, cancellationToken)
            };

            Message sentMessage = await action;
            _logger.LogInformation($"Пользователь: {message?.From?.Username} написал сообщение в {message?.Chat.Id}:{sentMessage.MessageId}");
        }


        private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            var message = callbackQuery.Message;

            switch (callbackQuery.Data)
            {
                case LangRoute.Russia:
                    _localizationService.setLocale("ru");
                    await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id, _localizationService.GetLocalizedString("choice"));
                    await _baseCommand.Auth(_botClient, message, cancellationToken);
                    break;
                case LangRoute.English:
                    _localizationService.setLocale("en");
                    await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id, _localizationService.GetLocalizedString("choice"));
                    await _baseCommand.Auth(_botClient, message, cancellationToken);
                    break;
                case AuthRoute.User:
                    await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id, $"Пользователь авторизирован: User");
                    await _baseCommand.Account(_botClient, message, cancellationToken);
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
