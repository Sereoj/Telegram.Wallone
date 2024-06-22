namespace Telegram.Wallone.Services
{
    public class LocalizationService
    {
        private readonly Dictionary<string, Dictionary<string, string>> _localizationData;
        private string _language = "en";

        public LocalizationService()
        {
            _localizationData = new Dictionary<string, Dictionary<string, string>>
            {
                ["en"] = new Dictionary<string, string>
                {
                    ["start"] = "Please select the desired language to work with the bot. Thanks ;)",
                    
                    ["wellcome"] = "Hello, I'm Lona! I am very cute and pretty, " +
                    "I will notify you about the purchased images by users, " +
                    "as well as help you with many questions ;)",
                    
                    ["auth"] = "So, let's log in with you to use your personal account." +
                    "Click on the link and copy the data in the **user:key format**\n" +
                    "And send a message in the format:\n**/auth user:key**\n\n" +
                    "Attention beware of scammers, be sure to check the address of the bot @wallone_bot\n\n" +
                    "In no case, do not show or give the key to third parties.",
                    ["auth.check"] = "Check",
                    
                    ["for_new_users"] = "I have prepared a gift for you, click \"pick up\"",
                    
                    ["account"] = ", my friend, all the necessary information about your account is displayed here.\n\nUsername: [username]\nVerification:false\n\n__Posts:__\nPublished: 100\nOn moderation: 9\n",
                    ["account.popular_images"] = "Popular Images",
                    ["account.recently_purchased_images"] = "Recently purchased images",
                    ["account.balance"] = "Balance",
                    
                    ["greeting"] = "Hello!",

                    ["language"] = "Select a language:",
                    ["language.english"] = "English",
                    ["language.russia"] = "Русский",
                    
                    ["choice"] = "Your choice has been received!",
                },
                ["ru"] = new Dictionary<string, string>
                {
                    ["start"] = "Пожалуйста, выберите нужный язык для работы с ботом. Спасибо ;) ",
                    
                    ["wellcome"] = "Здравствуй, я Лона! Я очень миленькая и хорошенькая, " +
                    "буду оповещать тебя о купленных изображениях пользователями, " +
                    "а так же помогу тебе в многих вопросах ;)",
                    
                    ["auth"] = "Итак, давай с тобой авторизируйся, чтобы воспользоваться личным кабинетом. " +
                    "Перейди по ссылке и скопируй данные в формате **user:key**\n" +
                    "И отправь сообщение в формате:\n**/auth user:key**\n\n" +
                    "Внимание остерегайся мошенников, обязательно проверь адрес бота @wallone_bot\n\n" + 
                    "Ни в коем случае, не показывай и не давай ключ третьим лицам.",
                    ["auth.check"] = "Проверить",
                    
                    ["for_new_users"] = "Я подготовила тебе подарок, нажми \"забрать\"",
                    
                    ["account"] = ", мой друг, здесь отображается вся необходимая информация о твоем аккаунте.\n\nИмя пользователя: [username]\nВерификация:false\n\n__Посты:__\nОпубликованные: 100\nНа модерации: 9\n",
                    ["account.popular_images"] = "Популярные изображения",
                    ["account.recently_purchased_images"] = "Недавно купленные изображения",
                    ["account.balance"] = "Баланс",

                    ["select"] = "Выберите язык:",
                    ["language.english"] = "English",
                    ["language.russia"] = "Русский",
                    
                    ["greeting"] = "Привет!",
                    ["choice"] = "Ваш выбор получен!",
                }
            };
        }


        public void setLocale(string lang)
        {
            _language = lang;
        }
        public string GetLocalizedString(string key)
        {
            if (_localizationData.TryGetValue(_language, out var languageData))
            {
                if (languageData.TryGetValue(key, out var localizedString))
                {
                    return localizedString;
                }
            }

            return $"[{key}]";
        }
    }
}
