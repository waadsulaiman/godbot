using GodBot.Core.Interfaces;
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
        }
    }
}