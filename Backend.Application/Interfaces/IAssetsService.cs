using Backend.Application.DTOs;

namespace Backend.Application.Interfaces
{
    public interface IAssetsService : IEntityService<AssetsDTO>
    {
        public Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOByWalletIdAsync(int walletId);
    }
}