using Application;
using Microsoft.OpenApi.Models;
using Persistence.Persistence;

namespace WebApi.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(string.Format(@"{0}\MyFileStorage.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MyFileStorage",
                    Description = "Application for hosting and sharing files",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Md Shaker Ibna Kamal",
                        Url = new Uri("https://www.linkedin.com/in/shaker-ibna-kamal/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Demo License",
                        Url = new Uri("https://example.com/license")
                    }
                });
            });
        }
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddApplication();
        }
        public static void ConfigurePersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
        }
    }
}
