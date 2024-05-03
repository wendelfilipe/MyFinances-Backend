using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Entites.Enums;

namespace Backend.Domain.Interfaces.AssetsInterface
{
    public interface IAssetsRepository : IEntityRepository<Assets>
    {
        public Task<IEnumerable<Assets>> GetAllAssetsByWalletIdAsync(int userId);

        public Task<IEnumerable<Assets>> GetStocksByWalletIdAndTypeAssets(int walletId); 
    }
}