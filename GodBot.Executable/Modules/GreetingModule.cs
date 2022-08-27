using Discord.Commands;

namespace GodBot.Executable.Modules
{
    public class GreetingModule : ModuleBase<SocketCommandContext>
    {
        #region GreetAsync

        [Command("Hi")]
        [Alias("Hello")]
        public async Task GreetAsync() =>
            await ReplyAsync($"Hi there! **{Context.User.Username}**");

        #endregion GreetAsync
    }
}