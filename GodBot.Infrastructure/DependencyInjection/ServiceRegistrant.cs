using GodBot.Core.Interfaces;
using GodBot.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GodBot.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Used for registring the remaining minor services.
    /// </summary>
    public class ServiceRegistrant : IServiceRegistrant
    {
        public void Register(IServiceCollection services)
        {
            services.AddSingleton<IHumorService, HumorService>();
        }
    }
}