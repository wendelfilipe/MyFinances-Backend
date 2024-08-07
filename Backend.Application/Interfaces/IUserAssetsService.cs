using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.DTOs;

namespace Backend.Application.Interfaces
{
    public interface IUserAssetsService
    {
        Task<IEnumerable<UserAssetsDTO>> GetAllUserAssetsByWalletId(int walletId);
        Task CreateUserAssetsAsync(UserAssetsDTO userAssetsDTO);
        Task UpdateUserAssetsAsync(UserAssetsDTO userAssetsDTO);

    }
}