using Factory;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace WebAPI
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
            public Startup(IConfiguration configuration) => Configuration = configuration;

            public IConfiguration Configuration { get; }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers();
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });
                });

                ServiceFactory factory = new ServiceFactory(services);
                factory.AddCustomServices();
                factory.AddDbContextService();

                services.AddSwaggerGen(c =>
                {
                    c.EnableAnnotations();
                });
            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API V1");
                    c.RoutePrefix = string.Empty;
                });
                app.UseCors(builder =>
                {
                    builder.AllowAnyOrigin()                    // Specify allowed origins
                           .AllowAnyHeader()                   // Allow any header
                           .AllowAnyMethod();                  // Allow any HTTP method
                });
            app.UseHttpsRedirection();
                app.UseRouting();
                app.UseAuthorization();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }

