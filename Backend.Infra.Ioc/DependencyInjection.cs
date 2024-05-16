using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
using Backend.Infra.Data.Reporitories;
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

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IAssetsRepository, AssetsRepository>();
            services.AddScoped<IUserAssetsRepository, UserAssetsRepository>();

            services.AddScoped<IUserService, UserService>(implementationFactory =>
            {
                var mapper = implementationFactory.GetService<IMapper>();
                var userRepository = implementationFactory.GetService<IUserRepository>();
                
                return new UserService(userRepository, mapper, userRepository);
            });
            services.AddScoped<IAssetsService, AssetsService>(implementationFactory =>
            {
                var mapper = implementationFactory.GetService<IMapper>();
                var assetsRepository = implementationFactory.GetService<IAssetsRepository>();

                return new AssetsService(assetsRepository, mapper, assetsRepository);
            });
            services.AddScoped<IWalletService, WalletService>(implementationFactory =>
            {
                var mapper = implementationFactory.GetService<IMapper>();
                var walletRepository = implementationFactory.GetService<IWalletRepository>();

                return new WalletService(walletRepository, mapper, walletRepository);
            });
            services.AddScoped<IUserAssetsService, UserAssetsService>(implementationFactory =>
            {
                var mapper = implementationFactory.GetService<IMapper>();
                var userAssetsRepository = implementationFactory.GetService<IUserAssetsRepository>();

                return new UserAssetsService(userAssetsRepository, mapper,  userAssetsRepository);
            });
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}