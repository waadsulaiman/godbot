using Discord.Commands;

namespace GodBot.Executable.Commands
{
    public class GreetingCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Hi")]
        [Alias("Hello")]
        public async Task GreetAsync() =>
            await ReplyAsync($"Hi there! **{Context.User.Username}**");
    }
}