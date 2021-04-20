using ck.assecor.assessment_backend.infrastructure.Interfaces;
using ck.assecor.assessment_backend.infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ck.assecor.assessment_backend.api.Extensions
{
    internal static class ServiceBuilderExtension
    {
        /// <summary>
        /// Adds all services to the dependency container
        /// </summary>
        internal static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            return services;
        }
    }
}
