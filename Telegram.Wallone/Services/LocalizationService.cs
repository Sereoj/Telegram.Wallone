using Telegram.Wallone.Routes;

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

                    ["auth"] = "So, let's log in with you to use your personal account.\n" +
                    "Click on the link and copy the data in the user:key format\n\n" +
                    "And send a message in the format:\n `/auth user:key`\n\n" +
                    $"Attention beware of scammers, be sure to check the address of the bot [{LinkRoute.LinkBot}]({LinkRoute.LinkBot})\n\n" +
                    "In no case, do not show or give the key to third parties.",
                    ["auth.check"] = "Check",
                    ["auth.true"] = "The user is logged in",

                    ["subs_group"] = $"💬 [Subscribe to our channel]({LinkRoute.InviteLinkGroup})," +
                    $"to keep up to date with new updates and other news related to [Wallone]({LinkRoute.SiteLink}).",
                    ["subs_group.sub"] = "Subscribe to the channel",
                    ["subs_group.check"] = "Check",

                    ["for_new_users"] = "I have prepared a gift for you, click \"pick up\"",
                    
                    ["account"] =
                    "Welcome, ***username*** 🌟! You are here again to shine your light and delight us with your smiles.\n\n" +
                    "[Profile](https://link.app/users/username )\n" +
                    "├ Username: ***username***\n" +
                    "├ Tariff: Lite\n" +
                    "├ Role: User\n" +
                    "└ Balance: 100 tokens\n\n" +
                    "🏞 Posts:\n" +
                    "├ Total: ***109*** \n" +
                    "├ Published: ***100*** \n" +
                    "├ On moderation: ***9*** \n" +
                    "└ Rejected: ***0*** \n\n" +
                    "📋 Useful:\n" +
                    "├ News\n" +
                    "├ Telegram channel\n" +
                    "├ VK Group\n" +
                    "├ Help Department\n" +
                    "└ Publication rules",
                    ["account.popular_images"] = "Popular Images",
                    ["account.recently_purchased_images"] = "Recently purchased images",
                    
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
                    
                    ["auth"] = "Итак, давай с тобой авторизируйся, чтобы воспользоваться личным кабинетом.\n" +
                    "Перейди по ссылке и скопируй данные в формате user:key\n\n" +
                    "И отправь сообщение в формате:\n `/auth user:key` \n\n" +
                    $"Внимание остерегайся мошенников, обязательно проверь адрес бота [{LinkRoute.LinkBot}]({LinkRoute.LinkBot})\n\n" + 
                    "Ни в коем случае, не показывай и не давай ключ третьим лицам.",
                    ["auth.check"] = "Проверить",
                    ["auth.true"] = "Пользователь авторизирован",

                    ["subs_group"] = $"💬 [Подпишитесь на наш канал]({LinkRoute.InviteLinkGroup})," +
                    $"чтобы быть в курсе новых обновлений и других новостей, связанных с [Wallone]({LinkRoute.SiteLink}).",
                    ["subs_group.sub"] = "Подписаться на канал",
                    ["subs_group.check"] = "Проверить",

                    ["for_new_users"] = "Я подготовила тебе подарок, нажми \"забрать\"",
                    
                    ["account"] = "Добро пожаловать, username 🌟! Вы снова здесь, чтобы сиять своим светом и радовать нас своими улыбками.\n\n" +
                    "[Профиль](https://link.app/users/username)\n" +
                    "├ Имя пользователя: ***username***\n" +
                    "├ Тариф: Lite\n" +
                    "├ Роль: Пользователь\n" +
                    "└ Баланс: 100 токенов\n\n" +
                    "🏞 Постов:\n" +
                    "├ Всего: ***109*** \n" +
                    "├ Опубликованные: ***100***\n" +
                    "├ На модерации: ***9*** \n" +
                    "└ Отклоненные: ***0***\n\n" +
                    "📋 Полезное:\n" +
                    "├ [@wallone_news](@wallone_news)\n" +
                    "├ [@wallone_channel](@wallone_channel)\n" +
                    "├ [Группа ВК](https://vk.com/wallone_group)\n" +
                    "├ [Отдел помощи](https://wallone.app/help)\n" +
                    "└ [Правила публикации](https://wallone.app/rules)",
                    ["account.popular_images"] = "Популярные изображения",
                    ["account.recently_purchased_images"] = "Недавно купленные изображения",

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
