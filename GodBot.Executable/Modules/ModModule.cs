using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace GodBot.Executable.Modules
{
    public class ModModule : ModuleBase<SocketCommandContext>
    {
        #region KickAsync

        [Command("kick")]
        [Summary("Kick a user from the server.")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickAsync(SocketGuildUser targetUser, [Remainder] string reason = "No reason provided.")
        {
            await targetUser.KickAsync(reason);
            await ReplyAsync($"**{targetUser}** has been kicked. Bye bye :wave:");
        }

        #endregion KickAsync

        #region BanAsync

        [Command("ban")]
        [Summary("Ban a user from the server")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanAsync(SocketGuildUser targetUser, [Remainder] string reason = "No reason provided.")
        {
            await Context.Guild.AddBanAsync(targetUser.Id, 0, reason);
            await ReplyAsync($"**{targetUser}** has been banned. Bye bye :wave:");
        }

        #endregion BanAsync

        #region UnbanAsync

        [Command("unban")]
        [Summary("Unban a user from the server")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task UnbanAsync(ulong targetUser)
        {
            await Context.Guild.RemoveBanAsync(targetUser);
            await Context.Channel.SendMessageAsync($"The user has been unbanned :clap:");
        }

        #endregion UnbanAsync

        #region DisconnectFromVoiceChannelAsync

        [Command("vkick")]
        [Summary("Disconnect a user from the voice channel.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task DisconnectFromVoiceChannelAsync(SocketGuildUser targetUser)
        {
            var voiceChannel = targetUser.VoiceChannel;
            await targetUser.VoiceChannel.DisconnectAsync();
            await ReplyAsync($"**{targetUser}** has been disconnected from {voiceChannel.Name}. Bye bye :wave:");
        }

        #endregion DisconnectFromVoiceChannelAsync
    }
}