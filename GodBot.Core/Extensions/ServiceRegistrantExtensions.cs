using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GodBot.Core.Interfaces;

namespace GodBot.Core.Extensions
{
    public static class ServiceRegistrantExtensions
    {
        /// <summary>
        /// An extension for registring services in assembly.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServicesFromAssemblies(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetEntryAndReferencedAssemblies();

            foreach (var assembly in assemblies)
            {
                var registrants = assembly.ExportedTypes.Where(x => typeof(IServiceRegistrant).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IServiceRegistrant>().ToList();

                registrants.ForEach(x => x.Register(services));
            }
        }
    }
}