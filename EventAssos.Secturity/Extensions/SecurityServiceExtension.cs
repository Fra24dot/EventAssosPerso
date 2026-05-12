using EventAssos.Core.Interfaces.Services.Auth;
using EventAssos.Core.Interfaces.Services.Tools;
using EventAssos.Core.Services;
using EventAssos.Security.Services.Auth;
using EventAssos.Security.Services.Tools;
using EventAssos.Security.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Security.Extensions
{
    public static class SecurityServiceExtension
    {
        public static void AddSecurityServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration
            .GetSection("JwtSettings")
            .Get<JwtSettings>();
            services.AddSingleton(jwtSettings);

            
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordHacherService, PasswordHacherService>();
            services.AddScoped<IPasswordGeneratorService, PasswordGeneratorService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
