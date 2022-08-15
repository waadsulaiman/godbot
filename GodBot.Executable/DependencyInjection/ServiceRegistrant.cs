using GodBot.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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
        }
    }
}