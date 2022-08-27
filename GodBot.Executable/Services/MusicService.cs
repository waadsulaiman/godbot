using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using Victoria;
using Victoria.Enums;
using Victoria.EventArgs;

namespace GodBot.Executable.Services
{
    public class MusicService
    {
        private readonly LavaNode _lavaNode;
        private readonly ILogger<LavaNode> _logger;
        private readonly ConcurrentDictionary<ulong, CancellationTokenSource> _disconnectTokens;

        public MusicService(LavaNode lavaNode, ILogger<LavaNode> logger)
        {
            _lavaNode = lavaNode;
            _logger = logger;
            _disconnectTokens = new ConcurrentDictionary<ulong, CancellationTokenSource>();

            _lavaNode.OnLog += arg =>
            {
                _logger.Log(LogLevel.Critical, arg.Exception, arg.Message);
                return Task.CompletedTask;
            };

            _lavaNode.OnPlayerUpdated += OnPlayerUpdated;
            _lavaNode.OnStatsReceived += OnStatsReceived;
            _lavaNode.OnTrackEnded += OnTrackEnded;
            _lavaNode.OnTrackStarted += OnTrackStarted;
            _lavaNode.OnTrackException += OnTrackException;
            _lavaNode.OnTrackStuck += OnTrackStuck;
            _lavaNode.OnWebSocketClosed += OnWebSocketClosed;
        }

        #region OnPlayerUpdated

        private Task OnPlayerUpdated(PlayerUpdateEventArgs arg)
        {
            _logger.LogInformation($"Track update received for {arg.Track?.Title}: {arg.Position ?? TimeSpan.Zero}");
            return Task.CompletedTask;
        }

        #endregion OnPlayerUpdated

        #region OnStatsReceived

        private Task OnStatsReceived(StatsEventArgs arg)
        {
            _logger.LogInformation($"Lavalink has been up for {arg.Uptime}.");
            return Task.CompletedTask;
        }

        #endregion OnStatsReceived

        #region OnTrackStarted

        private async Task OnTrackStarted(TrackStartEventArgs arg)
        {
            await arg.Player.TextChannel.SendMessageAsync($"Now playing: {arg.Track.Title}");
            if (!_disconnectTokens.TryGetValue(arg.Player.VoiceChannel.Id, out var value))
            {
                return;
            }

            if (value.IsCancellationRequested)
            {
                return;
            }

            value.Cancel(true);
            await arg.Player.TextChannel.SendMessageAsync("Auto disconnect has been cancelled!");
        }

        #endregion OnTrackStarted

        #region OnTrackEnded

        private async Task OnTrackEnded(TrackEndedEventArgs args)
        {
            if (args.Reason != TrackEndReason.Finished)
            {
                return;
            }

            var player = args.Player;
            if (!player.Queue.TryDequeue(out var lavaTrack))
            {
                await player.TextChannel.SendMessageAsync("Queue completed! Please add more tracks to rock n' roll!");
                _ = InitiateDisconnectAsync(args.Player, TimeSpan.FromSeconds(60));
                return;
            }

            if (lavaTrack is null)
            {
                await player.TextChannel.SendMessageAsync("Next item in queue is not a track.");
                return;
            }

            await args.Player.PlayAsync(lavaTrack);
            await args.Player.TextChannel.SendMessageAsync(
                $"{args.Reason}: {args.Track.Title}\nNow playing: {lavaTrack.Title}");
        }

        #endregion OnTrackEnded

        #region InitiateDisconnectAsync

        private async Task InitiateDisconnectAsync(LavaPlayer player, TimeSpan timeSpan)
        {
            if (!_disconnectTokens.TryGetValue(player.VoiceChannel.Id, out var value))
            {
                value = new CancellationTokenSource();
                _disconnectTokens.TryAdd(player.VoiceChannel.Id, value);
            }
            else if (value.IsCancellationRequested)
            {
                _disconnectTokens.TryUpdate(player.VoiceChannel.Id, new CancellationTokenSource(), value);
                value = _disconnectTokens[player.VoiceChannel.Id];
            }

            await player.TextChannel.SendMessageAsync($"Auto disconnect initiated! Disconnecting in {timeSpan}...");
            var isCancelled = SpinWait.SpinUntil(() => value.IsCancellationRequested, timeSpan);
            if (isCancelled)
            {
                return;
            }

            await _lavaNode.LeaveAsync(player.VoiceChannel);
            await player.TextChannel.SendMessageAsync("Invite me again sometime, sugar.");
        }

        #endregion InitiateDisconnectAsync

        #region OnTrackException

        private async Task OnTrackException(TrackExceptionEventArgs arg)
        {
            _logger.LogError($"Track {arg.Track.Title} threw an exception. Please check Lavalink console/logs.");
            arg.Player.Queue.Enqueue(arg.Track);
            await arg.Player.TextChannel.SendMessageAsync(
                $"{arg.Track.Title} has been re-added to queue after throwing an exception.");
        }

        #endregion OnTrackException

        #region OnTrackStuck

        private async Task OnTrackStuck(TrackStuckEventArgs arg)
        {
            _logger.LogError(
                $"Track {arg.Track.Title} got stuck for {arg.Threshold}ms. Please check Lavalink console/logs.");
            arg.Player.Queue.Enqueue(arg.Track);
            await arg.Player.TextChannel.SendMessageAsync(
                $"{arg.Track.Title} has been re-added to queue after getting stuck.");
        }

        #endregion OnTrackStuck

        #region OnWebSocketClosed

        private Task OnWebSocketClosed(WebSocketClosedEventArgs arg)
        {
            _logger.LogCritical($"Discord WebSocket connection closed with following reason: {arg.Reason}");
            return Task.CompletedTask;
        }

        #endregion OnWebSocketClosed
    }
}