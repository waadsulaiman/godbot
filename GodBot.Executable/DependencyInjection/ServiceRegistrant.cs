using GodBot.Core.Interfaces;
using GodBot.Executable.Services;
using Microsoft.Extensions.DependencyInjection;
using Victoria;

namespace GodBot.Executable.DependencyInjection
{
    /// <summary>
    /// Used for registring the remaining minor services.
    /// </summary>
    public class ServiceRegistrant : IServiceRegistrant
    {
        public void Register(IServiceCollection services)
        {
            services.AddHostedService<CommandHandler>();
            services.AddLavaNode(x =>
            {
                x.LogSeverity = Discord.LogSeverity.Critical;
                x.SelfDeaf = true;
            });
            services.AddSingleton<LavaNode>();
            services.AddSingleton<LavaConfig>();
            services.AddSingleton<MusicService>();
        }
    }
}