using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Entites.UserAssetsEntity;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Entites.WalletEntites;
using Backend.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Data.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
<<<<<<< HEAD
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            // Outras configurações, como a cadeia de conexão, podem estar presentes aqui
        }

        public DbSet<User> Users { get; set; }
=======
        public DbSet<User> User{ get; set; }
>>>>>>> feature
        public DbSet<Wallet> Wallets  { get; set; }
        public DbSet<Assets> Assets { get; set; }

        public DbSet<UserAssets> UserAssets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}