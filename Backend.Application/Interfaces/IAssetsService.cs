using Backend.Application.DTOs;
using Backend.Domain.Entites.Enums;

namespace Backend.Application.Interfaces
{
    public interface IAssetsService : IEntityService<AssetsDTO>
    {
        Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOByWalletIdAsync(int walletId);
        Task<IEnumerable<AssetsDTO>> GetStocksByWalletId(int walletId);
        Task<IEnumerable<AssetsDTO>> GetFiisByWalletId(int walletId);
        Task<IEnumerable<AssetsDTO>> GetFixedByWalletId(int walletId);
        Task<IEnumerable<AssetsDTO>> GetInternacionalAssetsByWalletId(int walletId);
        Task<IEnumerable<UserAssetsDTO>> GetUserAssetsByWalletId(int walletId);
    }
}