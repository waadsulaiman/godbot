using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace GodBot.Executable.Commands
{
    public class ModCommands : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [Summary("Kick a user from the server.")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(SocketGuildUser targetUser, [Remainder] string reason = "No reason provided.")
        {
            await targetUser.KickAsync(reason);
            await ReplyAsync($"**{targetUser}** has been kicked. Bye bye :wave:");
        }

        [Command("ban")]
        [Summary("Ban a user from the server")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task Ban(SocketGuildUser targetUser, [Remainder] string reason = "No reason provided.")
        {
            await Context.Guild.AddBanAsync(targetUser.Id, 0, reason);
            await ReplyAsync($"**{targetUser}** has been banned. Bye bye :wave:");
        }

        [Command("unban")]
        [Summary("Unban a user from the server")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Unban(ulong targetUser)
        {
            await Context.Guild.RemoveBanAsync(targetUser);
            await Context.Channel.SendMessageAsync($"The user has been unbanned :clap:");
        }

        [Command("vkick")]
        [Summary("Disconnect a user from the voice channel.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task DisconnectFromVoiceChannel(SocketGuildUser targetUser)
        {
            var voiceChannel = targetUser.VoiceChannel;
            await targetUser.VoiceChannel.DisconnectAsync();
            await ReplyAsync($"**{targetUser}** has been disconnected from {voiceChannel.Name}. Bye bye :wave:");
        }
    }
}