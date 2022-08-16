using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Victoria;

namespace GodBot.Executable
{
    /// <summary>
    /// The class is responsible for handling the commands and various events.
    /// </summary>
    public class CommandHandler : DiscordClientService
    {
        private readonly CommandService _service;
        private readonly LavaNode _lavaNode;
        private readonly IServiceProvider _provider;

        public CommandHandler(DiscordSocketClient client, CommandService service, LavaNode lavaNode, IServiceProvider provider, ILogger<DiscordClientService> logger) : base(client, logger)
        {
            _service = service;
            _lavaNode = lavaNode;
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Client.Ready += OnReadyAsync;
            Client.MessageReceived += OnMessageReceived;
            _service.CommandExecuted += OnCommandExecuted;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task OnReadyAsync()
        {
            if (!_lavaNode.IsConnected)
            {
                await _lavaNode.ConnectAsync();
            }
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> commandInfo, ICommandContext commandContext, IResult result)
        {
            if (commandInfo.IsSpecified && !result.IsSuccess)
            {
                await commandContext.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        private async Task OnMessageReceived(SocketMessage socketMessage)
        {
            if (socketMessage is not SocketUserMessage message)
            {
                return;
            }

            if (message.Source != MessageSource.User)
            {
                return;
            }

            var argPos = 0;
            if (!message.HasStringPrefix(AppSettings.CommandPrefix, ref argPos) && !message.HasMentionPrefix(Client.CurrentUser, ref argPos))
            {
                return;
            }

            var context = new SocketCommandContext(Client, message);
            await _service.ExecuteAsync(context, argPos, _provider);
        }
    }
}