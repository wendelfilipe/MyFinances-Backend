using Backend.Application.DTOs;
using Backend.Domain.Entites.Enums;

namespace Backend.Application.Interfaces
{
    public interface IAssetsService
    {
        Task CreateAssetAsync(AssetsDTO assetsDTO);
        Task UpdateAssetAsync(AssetsDTO assetsDTO);
        Task<AssetsDTO> GetAssetByIdAsync(int id);
        Task<IEnumerable<AssetsDTO>> GetAllAssetsAsync();
        Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOByAssetIdAsync(int assetId);
        Task<IEnumerable<AssetsDTO>> GetStocksByAssetIdAsync(int assetId);
        Task<IEnumerable<AssetsDTO>> GetFiisByAssetIdAsync(int assetId); 
        Task<IEnumerable<AssetsDTO>> GetFixedByAssetIdAsync(int assetId);
        Task<IEnumerable<AssetsDTO>> GetInternacionalAssetsByAssetIdAsync(int assetId);
        Task<IEnumerable<AssetsDTO>> GetAllByIdsAsync(IEnumerable<int>? assetsDTO);
        
    }
}