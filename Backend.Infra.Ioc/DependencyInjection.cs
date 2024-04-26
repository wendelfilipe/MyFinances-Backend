using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Interfaces.AssetsInterface;
using Backend.Domain.Interfaces.UserInterface;
using Backend.Domain.Interfaces.WalletInterface;
using Backend.Infra.Data.Context;
using Backend.Infra.Data.EnititesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastruture(IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<AppDbContext>(options => 
                options.UseNpgsql(configuration.GetConnectionString("myfinancesapp_postgresql"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
            );

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IAssetsRepository, AssetsRepository>();

            return services;
        }
    }
}