using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Wallone.Validators
{
    internal class TokenValidator
    {
        public static bool Validator(string token)
        {
            if(!string.IsNullOrEmpty(token))
            {
                return token.Contains(':');
            }
            return false;
        }
    }
}
