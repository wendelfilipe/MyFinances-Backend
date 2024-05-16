using Backend.Application.DTOs;
using Backend.Domain.Entites.Enums;

namespace Backend.Application.Interfaces
{
    public interface IAssetsService : IEntityService<AssetsDTO>
    {
        Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOByAssetIdAsync(int assetId);
        Task<IEnumerable<AssetsDTO>> GetStocksByAssetId(int assetId);
        Task<IEnumerable<AssetsDTO>> GetFiisByAssetId(int assetId); 
        Task<IEnumerable<AssetsDTO>> GetFixedByAssetId(int assetId);
        Task<IEnumerable<AssetsDTO>> GetInternacionalAssetsByAssetId(int assetId);
        Task<IEnumerable<AssetsDTO>> GetAllByIdsAsync(IEnumerable<int>? entitysDTO);
        
    }
}