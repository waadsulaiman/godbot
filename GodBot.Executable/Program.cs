using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using GodBot.Core.Extensions;
using GodBot.Executable;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureDiscordHost((context, config) =>
    {
        config.SocketConfig = new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Info,
            AlwaysDownloadUsers = true,
            MessageCacheSize = 200,
            GatewayIntents = GatewayIntents.All,
        };

        config.Token = AppSettings.DiscordToken;
    })
    .UseCommandService((context, config) =>
    {
        config.DefaultRunMode = RunMode.Async;
        config.CaseSensitiveCommands = false;
    })
    .UseInteractionService((context, config) =>
    {
        config.LogLevel = LogSeverity.Info;
        config.UseCompiledLambda = true;
    })
    .ConfigureServices((_, services) =>
    {
        services.RegisterServicesFromAssemblies();
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();