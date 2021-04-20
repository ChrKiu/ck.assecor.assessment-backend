using ck.assecor.assessment_backend.api.Configurations;
using ck.assecor.assessment_backend.data.CsvContext;
using Microsoft.Extensions.DependencyInjection;

namespace ck.assecor.assessment_backend.api.Extensions
{
    
    internal static class ConfigurationBuilderExtension
    {
        /// <summary>
        /// Adds all configurations to the dependency container
        /// </summary>
        internal static IServiceCollection ConfigureConfigurations(this IServiceCollection services)
        {
            services.AddSingleton<ICsvConfiguration, CsvConfiguration>();
            return services;
        }
    }
}
