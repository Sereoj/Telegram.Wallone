using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Wallone.Controllers.Commands.Base
{
    public interface ICommand
    {
        Task<Message> ExecuteAsync(Message message, CancellationToken cancellationToken);
    }
}
