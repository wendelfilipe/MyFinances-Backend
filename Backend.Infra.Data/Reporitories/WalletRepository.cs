using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Interfaces.WalletInterface;
using Backend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Data.EnititesConfiguration
{
    public class WalletRepository : IWalletRepository
    {
        private readonly AppDbContext context;
        public WalletRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Wallet> CreateAsync(Wallet wallet)
        {
            context.Wallets.Add(wallet);
            await context.SaveChangesAsync();

            return wallet;
        }

        public async Task<Wallet> DeleteByIdAsync(Wallet wallet)
        {
            context.Wallets.Remove(wallet);
            await context.SaveChangesAsync();

            return wallet;
        }

        public async Task<IEnumerable<Wallet>> GetAllAsync()
        {
            return await context.Wallets.ToListAsync();
        }

        public async Task<Wallet> GetByIdAsync(int id)
        {
            return await context.Wallets.FindAsync(id);
        }

        public async Task<Wallet> UpdateAsync(Wallet user)
        {
            context.Wallets.Update(user);
            await context.SaveChangesAsync();

            return user;
        }
    }
}