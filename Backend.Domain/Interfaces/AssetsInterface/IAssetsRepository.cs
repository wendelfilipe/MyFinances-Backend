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

        public Task<IEnumerable<Assets>> GetStocksByWalletId(int walletId); 

        public Task<IEnumerable<Assets>> GetFiisByWalletId(int walletId);

        public Task<IEnumerable<Assets>> GetFixedByWalletId(int walletId);

        public Task<IEnumerable<Assets>> GetInternacionalAssetsByWalletId(int walletId);
    }
}