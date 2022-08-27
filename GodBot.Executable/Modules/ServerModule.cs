using Discord;
using Discord.Commands;

namespace GodBot.Executable.Module
{
    public class ServerModule : ModuleBase<SocketCommandContext>
    {
        #region GetServerInfoAsync

        [Command("info")]
        [Alias("server", "server-info", "s-info")]
        [Summary("Show server information.")]
        [RequireBotPermission(GuildPermission.EmbedLinks)]
        public async Task GetServerInfoAsync()
        {
            var users = Context.Guild.Users;
            EmbedBuilder embed = new EmbedBuilder()
                .WithColor(49, 133, 245)
                .WithTitle(Context.Guild.Name)
                .AddField("ID", Context.Guild.Id, true)
                .AddField("Owner", Context.Guild.Owner, true)
                .AddField("Created at", Context.Guild.CreatedAt, true)
                .AddField("Users", Context.Guild.Users.Count(x => !x.IsBot), true)
                .AddField("Bots", Context.Guild.Users.Count(x => x.IsBot), true)
                .AddField("Region", Context.Guild.VoiceRegionId, true)
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .WithCurrentTimestamp();

            await ReplyAsync(embed: embed.Build());
        }

        #endregion GetServerInfoAsync
    }
}