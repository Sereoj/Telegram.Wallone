using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Wallone.Controllers.Commands;
using Telegram.Wallone.Helpers;
using Telegram.Wallone.Services;



IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register Bot configuration
        services.Configure<BotConfiguration>(
        context.Configuration.GetSection(BotConfiguration.Configuration));
        services.AddHttpClient("wallone_bot")
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    try
                    {
                        BotConfiguration? botConfig = sp.GetConfiguration<BotConfiguration>();
                        TelegramBotClientOptions options = new(botConfig.BotToken);
                        return new TelegramBotClient(options, httpClient);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                });
        
        services.AddScoped<LocalizationService>();
        services.AddScoped<LangHelper>();
        services.AddScoped<BaseCommand>();
        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();
    })
    .Build();

await host.RunAsync();


public class BotConfiguration
{
    public static readonly string Configuration = "BotConfiguration";

    public string BotToken { get; set; } = "";
}
