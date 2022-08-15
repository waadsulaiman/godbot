using Microsoft.Extensions.DependencyInjection;

namespace GodBot.Core.Interfaces
{
    /// <summary>
    /// An interface for service registeration related operations.
    /// </summary>
    public interface IServiceRegistrant
    {
        public void Register(IServiceCollection services);
    }
}