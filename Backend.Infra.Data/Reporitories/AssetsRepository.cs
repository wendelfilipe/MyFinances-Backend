using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Entites.Enums;
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
            return await context.Assets.Where(a => a.WalletId == walletId ).ToListAsync();
        }

        public async Task<IEnumerable<Assets>> GetStocksByWalletIdAndTypeAssets(int walletId)
        {
            return await context.Assets.Where(a => a.WalletId == walletId && a.SourceTypeAssets == SourceTypeAssets.Stocks).ToListAsync();
        } 


    }
}