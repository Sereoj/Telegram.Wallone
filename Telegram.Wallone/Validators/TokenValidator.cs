using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Wallone.Validators
{
    internal static class TokenValidator
    {
        public static bool Validation(string messageText)
        {
            return messageText.Contains("/auth") && messageText.Contains(":");
        } 
    }
}
