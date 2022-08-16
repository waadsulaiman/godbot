using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace GodBot.Executable.Module
{
    public class UserModule : ModuleBase<SocketCommandContext>
    {
        [Command("userinfo")]
        [Alias("user", "user-info", "u-info")]
        [RequireBotPermission(GuildPermission.EmbedLinks)]
        public async Task GetUserInfoAsync(SocketGuildUser user = null)
        {
            if (user == null)
            {
                user = Context.User as SocketGuildUser;
            };

            EmbedBuilder embed = new EmbedBuilder()
                .WithColor(245, 65, 40)
                .WithTitle($"{user.Username}#{user.Discriminator}")
                .AddField("ID", user.Id, true)
                .AddField("Name", $"{user.Username}#{user.Discriminator}", true)
                .AddField("Created at", user.CreatedAt, true)
                .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .WithCurrentTimestamp();

            await ReplyAsync(embed: embed.Build());
        }
    }
}