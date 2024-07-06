using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Controllers.Commands.Event
{
    internal class EventCommand : Base.BaseCommand
    {
        public EventCommand(ITelegramBotClient botClient, LocalizationService localizationService, LangHelper langHelper) : base(botClient, localizationService, langHelper)
        {
        }

        public async override Task<Message> ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            return await Task.FromResult<Message>(null);
        }
    }
}
