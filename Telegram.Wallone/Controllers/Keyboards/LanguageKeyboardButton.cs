using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Routes;

namespace Telegram.Wallone.Controllers.Keyboards
{
    internal class LanguageKeyboardButton
    {
        private readonly LangHelper langHelper;

        public LanguageKeyboardButton(LangHelper _langHelper)
        {
            langHelper = _langHelper;
        }
        public InlineKeyboardMarkup GetInlineKeyboardMarkup()
        {
            return new(new[]
{
                InlineKeyboardButton.WithCallbackData(text: langHelper.getLanguage("language.russia"), callbackData: LangRoute.Russia),
                InlineKeyboardButton.WithCallbackData(text:  langHelper.getLanguage("language.english"), callbackData: LangRoute.English),
            });
        }
    }
}
