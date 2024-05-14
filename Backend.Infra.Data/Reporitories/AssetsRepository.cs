using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Entites.UserAssetsEntity;
using Backend.Domain.Interfaces;
using Backend.Domain.Interfaces.AssetsInterface;
using Backend.Infra.Data.Context;
using Backend.Infra.Data.Reporitories.EntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Data.EnititesConfiguration
{
    public class AssetsRepository : EntityRepository<Assets>, IAssetsRepository
    {
        private readonly AppDbContext context;
        public AssetsRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Assets>> GetAllAssetsByWalletIdAsync(int walletId)
        {
            return await context.Assets.AsNoTracking().Where(a => a.WalletId == walletId ).ToListAsync();
        }

        public async Task<IEnumerable<Assets>> GetFiisByWalletId(int walletId)
        {
            return await context.Assets.Where(a => a.WalletId == walletId && a.SourceTypeAssets == SourceTypeAssets.Fiis).ToListAsync();
        }

        public async Task<IEnumerable<Assets>> GetStocksByWalletId(int walletId)
        {
            return await context.Assets.Where(a => a.WalletId == walletId && a.SourceTypeAssets == SourceTypeAssets.Stocks).ToListAsync();
        }

         public async Task<IEnumerable<Assets>> GetFixedByWalletId(int walletId)
        {
            return await context.Assets.Where(a => a.WalletId == walletId && a.SourceTypeAssets == SourceTypeAssets.Fixed).ToListAsync();
        }

         public async Task<IEnumerable<Assets>> GetInternacionalAssetsByWalletId(int walletId)
        {
            return await context.Assets.Where(a => a.WalletId == walletId && a.SourceTypeAssets == SourceTypeAssets.InteralcionalAssets).ToListAsync();
        }

        public async Task<IEnumerable<UserAssets>> GetUserAssetsByWalletId(int walletId)
        {
            return await context.UserAssets.AsNoTracking().Where(ua => ua.WalletId == walletId).ToListAsync();
        }
    }
}