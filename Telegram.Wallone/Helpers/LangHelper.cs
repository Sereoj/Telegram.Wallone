using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Wallone.Services;

namespace Telegram.Wallone.Helpers
{
    public class LangHelper
    {
        private readonly LocalizationService _localizationService;

        public LangHelper(LocalizationService localizationService)
        {
            _localizationService = localizationService;
        }
        public string getLanguage(string code)
        {
            return _localizationService.GetLocalizedString(code);
        }
    }
}
