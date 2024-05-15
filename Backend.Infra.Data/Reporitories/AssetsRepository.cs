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

        public async Task<IEnumerable<Assets>> GetAllAssetsByAssetIdAsync(int assetId)
        {
            return await context.Assets.AsNoTracking().Where(a => a.Id == assetId ).ToListAsync();
        }

        public async Task<IEnumerable<Assets>> GetFiisByAssetId(int assetId)
        {
            return await context.Assets.Where(a => a.Id == assetId && a.SourceTypeAssets == SourceTypeAssets.Fiis).ToListAsync();
        }

        public async Task<IEnumerable<Assets>> GetStocksByAssetId(int assetId)
        {
            return await context.Assets.Where(a => a.Id == assetId && a.SourceTypeAssets == SourceTypeAssets.Stocks).ToListAsync();
        }

         public async Task<IEnumerable<Assets>> GetFixedByAssetId(int assetId)
        {
            return await context.Assets.Where(a => a.Id == assetId && a.SourceTypeAssets == SourceTypeAssets.Fixed).ToListAsync();
        }

         public async Task<IEnumerable<Assets>> GetInternacionalAssetsByAssetId(int assetId)
        {
            return await context.Assets.Where(a => a.Id == assetId && a.SourceTypeAssets == SourceTypeAssets.InteralcionalAssets).ToListAsync();
        }

        public async Task<IEnumerable<UserAssets>> GetUserAssetsByAssetId(int assetId)
        {
            return await context.UserAssets.AsNoTracking().Where(ua => ua.Id == assetId).ToListAsync();
        }

        public async Task<IEnumerable<Assets>> GetAllByIdsAsync(IEnumerable<int> entitysDTO)
        {
            return await context.Assets.AsNoTracking().Where(a => entitysDTO.Contains(a.Id)).ToListAsync();
        }
    }
}