using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using ck.assecor.assessment_backend.data.CsvContext;
using ck.assecor.assessment_backend.data.EfContext;
using ck.assecor.assessment_backend.data.Mapping;
using ck.assecor.assessment_backend.data.Repositories;
using ck.assecor.assessment_backend.infrastructure.Models.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ck.assecor.assessment_backend.api.Extensions
{
    internal static class DataSourceBuilderExtension
    {
        /// <summary>
        ///     Sets up the container to use CSV-Files as datasource
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigurePersonCsvDataSource(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, CsvPersonRepository>();
            services.AddSingleton<CsvPersonContext>();
            return services;
        }

        /// <summary>
        ///     Sets up the container to use a DB-Connection with Entityframework as datasource
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigurePersonEfDataSource(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IPersonRepository, EfPersonRepository>();
            
            services.AddDbContext<EfDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        /// <summary>
        ///     Configures the mapping profiles in the container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureProfileMapping(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddMaps(Assembly.GetAssembly(typeof(PersonProfile)));
                cfg.AddExpressionMapping();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
