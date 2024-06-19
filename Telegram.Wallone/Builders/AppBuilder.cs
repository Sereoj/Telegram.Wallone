using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Wallone.Builders.Base;
using Telegram.Wallone.Controllers;
using Telegram.Wallone.Exceptions;
using Telegram.Wallone.Interfaces;
using Telegram.Wallone.Repositories;
using Telegram.Wallone.Services.Loggings;
using Telegram.Wallone.Services.Settings;
using Telegram.Wallone.Validators;

namespace Telegram.Wallone.Builders
{
    internal class AppBuilder : BaseBuilder
    {
        private IBuilder appSettings;

        public AppBuilder Query(IBuilder interfaces)
        {
            appSettings = interfaces;
            return this;
        }

        internal void Start()
        {
            try{
                
                var token = SettingsService.GetToken();
                if(TokenValidator.Validator(token))
                {
                    ConsoleLogService.Send("Приложение запущено :D", Models.MessageType.Information, typeof(AppBuilder));
                    
                    TelegramRepository.Set(new TelegramBotClient(token));
                    new BaseTelegramController(TelegramRepository.Get());

                }
                else
                {
                    throw new SettingsException("Не удалось распознать токен :(");
                }

            }catch(Exception ex)
            {
                ConsoleLogService.Send("Не удалось запустить приложение :(", Models.MessageType.Error, typeof(AppBuilder));
                ConsoleLogService.Send(ex.Message, Models.MessageType.Error, typeof(AppBuilder));
            }
        }
    }
}
