using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Entites.UserAssetsEntity;

namespace Backend.Domain.Interfaces.AssetsInterface
{
    public interface IAssetsRepository : IEntityRepository<Assets>
    {
        public Task<IEnumerable<Assets>> GetAllAssetsByAssetIdAsync(int userId);

        public Task<IEnumerable<Assets>> GetStocksByAssetId(int assetId); 

        public Task<IEnumerable<Assets>> GetFiisByAssetId(int assetId);

        public Task<IEnumerable<Assets>> GetFixedByAssetId(int assetId);

        public Task<IEnumerable<Assets>> GetInternacionalAssetsByAssetId(int assetId);
        
        public Task<IEnumerable<UserAssets>> GetUserAssetsByAssetId(int assetId);

        public Task<IEnumerable<Assets>> GetAllByIdsAsync(IEnumerable<int> entitysDTO);
    }
}