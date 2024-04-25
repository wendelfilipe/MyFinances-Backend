using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Interfaces.AssetsInterface;
using Backend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Data.EnititesConfiguration
{
    public class AssetsRepository : IAssetsRepository
    {
        private readonly AppDbContext context;
        public AssetsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Assets> CreateAsync(Assets assets)
        {
            context.Assets.Add(assets);
            await context.SaveChangesAsync();

            return assets;
        }

        public async Task<Assets> DeleteByIdAsync(Assets assets)
        {
            context.Assets.Remove(assets);
            await context.SaveChangesAsync();

            return assets;
        }

        public async Task<IEnumerable<Assets>> GetAllAsync()
        {
            return await context.Assets.ToListAsync();
        }

        public async Task<Assets> GetByIdAsync(int id)
        {
            return await context.Assets.FindAsync(id);
        }

        public async Task<Assets> UpdateAsync(Assets wallet)
        {
            context.Assets.Update(wallet);
            await context.SaveChangesAsync();

            return wallet;
        }
    }
}