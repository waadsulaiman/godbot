using Discord.Commands;
using GodBot.Core.Interfaces;

namespace GodBot.Executable.Modules
{
    public class HumorModule : ModuleBase<SocketCommandContext>
    {
        private readonly IHumorService _service;

        public HumorModule(IHumorService service)
        {
            _service = service;
        }

        #region GetRandomJokeAsync

        [Command("joke")]
        public async Task GetRandomJokeAsync()
        {
            var result = await _service.GetRandomJokeAsync();
            await ReplyAsync(string.IsNullOrEmpty(result) ? "It seems like, I'm unable to find a joke." : result);
        }

        #endregion GetRandomJokeAsync

        #region GetRandomMemeAsync

        [Command("meme")]
        public async Task GetRandomMemeAsync()
        {
            var result = await _service.GetRandomMemeAsync();
            await ReplyAsync(string.IsNullOrEmpty(result) ? "It seems like, I'm unable to find a meme." : result);
        }

        #endregion GetRandomMemeAsync
    }
}