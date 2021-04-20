using ck.assecor.assessment_backend.api.ErrorHandling;
using ck.assecor.assessment_backend.api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ck.assecor.assessment_backend.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureConfigurations();

            var usedDataSource = this.Configuration.GetValue<string>("DataSources:ActiveDataSource");
            if(usedDataSource == "EF")
            {
                var connectionString = this.Configuration.GetConnectionString("PersonDb");
                services.ConfigurePersonEfDataSource(connectionString);
            } else if(usedDataSource == "CSV")
            {
                services.ConfigurePersonCsvDataSource();
            }         

            services.ConfigureProfileMapping();

            services.ConfigureApplicationServices();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ck.assecor.assessment_backend.api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ck.assecor.assessment_backend.api v1"));
            }
            
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
