using EventAssos.Core.Interfaces.Repositories.Data;
using EventAssos.Infrastructure.Database.Context;
using EventAssos.Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EventAssosContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();

        }
    }
}
