using Backend.Application.DTOs;
using Backend.Domain.Entites.Enums;

namespace Backend.Application.Interfaces
{
    public interface IAssetsService : IEntityService<AssetsDTO>
    {
        Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOByWalletIdAsync(int walletId);
        Task<IEnumerable<AssetsDTO>> GetStocksByWalletIdAndTypeAssets(int walletId);
    }
}