using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.DTOs;

namespace Backend.Application.Interfaces
{
    public interface IUserAssetsService : IEntityService<UserAssetsDTO>
    {
        
        Task<IEnumerable<UserAssetsDTO>> GetAllUserAssetsByWalletId(int walletId);

    }
}