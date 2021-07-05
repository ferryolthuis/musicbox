using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace RuleOneInvesting.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MusicBox API",
                    Version = "v1",
                    Description = "A REST API to administrate the wonderful genre of Rock Music",
                    Contact = new OpenApiContact
                    {
                        Name = "Ferry Olthuis",
                        Url = new Uri("https://github.com/justferro")
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder builder)
        {
            builder.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicBox API");
                options.DocumentTitle = "MusicBox API";
            });
            return builder;
        }
    }
}
