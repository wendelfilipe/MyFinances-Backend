using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.UserAssetsEntity;
using Backend.Domain.Interfaces.UserInterface;
using Backend.Infra.Data.Context;
using Backend.Infra.Data.Reporitories.EntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Data.Reporitories
{
    public class UserAssetsRepository : EntityRepository<UserAssets>, IUserAssetsRepository
    {
        private readonly AppDbContext context;
        public UserAssetsRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<UserAssets>> GetAllUserAssetsByWalletId(int walletId)
        {
            return await context.UserAssets.AsNoTracking().Where(ua => ua.WalletId == walletId).ToListAsync();
        }
    }
}