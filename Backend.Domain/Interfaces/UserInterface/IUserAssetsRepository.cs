using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.UserAssetsEntity;

namespace Backend.Domain.Interfaces.UserInterface
{
    public interface IUserAssetsRepository :IEntityRepository<UserAssets>
    {
        
        Task<IEnumerable<UserAssets>> GetAllUserAssetsByWalletId(int walletId);
    }
}