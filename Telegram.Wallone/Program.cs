

using Telegram.Wallone.Builders;
using Telegram.Wallone.Models;

new AppBuilder()
    .Query(
        new SettingsBuilder()
        .CreateOrUpdateFile("Settings/App.Json")
        ) 
    .Query(new LogBuilder())
    .Start();
