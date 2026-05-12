using EventAssos.Core.Interfaces.Services.Tools;
using EventAssos.Core.Services;
using EventAssos.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Extensions
{
    public static class CoreServiceExtension
    {
        public static void AddCoreServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var emailSettings = configuration
            .GetSection("EmailSettings").Get<EmailSettings>();
            services.AddSingleton(emailSettings);

            services.AddScoped<IEmailService, EmailService>();
        }

    }
}
