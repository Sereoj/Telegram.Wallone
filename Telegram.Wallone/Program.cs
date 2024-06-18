

using Telegram.Wallone.Builders;
using Telegram.Wallone.Models;

new AppBuilder()
    .LoadService(new SettingsBuilder(new Settings), typeof(Settings))
    .LoadService(new LogBuilder(new Log), typeof(Log))
    .Start();
