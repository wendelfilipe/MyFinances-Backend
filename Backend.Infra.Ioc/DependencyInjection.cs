using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Application.Mapping;
using Backend.Application.Services;
using Backend.Application.Services.EntityServices;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Interfaces;
using Backend.Domain.Interfaces.AssetsInterface;
using Backend.Domain.Interfaces.UserInterface;
using Backend.Domain.Interfaces.WalletInterface;
using Backend.Infra.Data.Context;
using Backend.Infra.Data.EnititesConfiguration;
using Backend.Infra.Data.Reporitories.EntityRepository;
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

            services.AddScoped<IEntityRepository<User>, IEntityRepository<User>>();
            services.AddScoped<IEntityRepository<Wallet>, IEntityRepository<Wallet>>();
            services.AddScoped<IEntityRepository<Assets>, IEntityRepository<Assets>>();
            services.AddScoped(typeof(IEntityService<UserDTO>), typeof(EntityService<UserDTO>));
            services.AddScoped(typeof(IEntityService<WalletDTO>), typeof(EntityService<WalletDTO>));
            services.AddScoped(typeof(IEntityService<AssetsDTO>), typeof(EntityService<AssetsDTO>));
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}