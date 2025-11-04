using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoDev.Infrastructure.Extension
{
    public static class CorsExtension
    {
        private const string DefaultCorsPolicyName = "DefaultCorsPolicy";
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, policy =>
                {
                    var origin = configuration["Cors:Origins"];
                    if (!string.IsNullOrEmpty(origin)) 
                    {
                        policy.WithOrigins(origin)
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                    }
                    
                });
            });
            return services;
        }
    }
}
