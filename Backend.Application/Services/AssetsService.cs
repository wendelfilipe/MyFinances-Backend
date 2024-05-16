using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Application.Services.EntityServices;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Interfaces;
using Backend.Domain.Interfaces.AssetsInterface;

namespace Backend.Application.Services
{
    public class AssetsService : EntityService<Assets, AssetsDTO>, IAssetsService
    {
        private readonly IAssetsRepository assetsRepository;
        private readonly IMapper mapper;
        public AssetsService(IEntityRepository<Assets> entityRepository, IMapper mapper, IAssetsRepository assetsRepository) : base(entityRepository, mapper)
        {
            this.assetsRepository = assetsRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOByAssetIdAsync(int assetId)
        {
            var assetsEntity = await assetsRepository.GetAllAssetsByAssetIdAsync(assetId);
            return mapper.Map<IEnumerable<AssetsDTO>>(assetsEntity);
        }
         public async Task<IEnumerable<AssetsDTO>> GetStocksByAssetId(int assetId)
        {
            var stocks = await assetsRepository.GetStocksByAssetId(assetId);
            return mapper.Map<IEnumerable<AssetsDTO>>(stocks);
        }
        public async Task<IEnumerable<AssetsDTO>> GetFiisByAssetId(int assetId)
        {
            var fiis = await assetsRepository.GetFiisByAssetId(assetId);
            return mapper.Map<IEnumerable<AssetsDTO>>(fiis);
        }
        public async Task<IEnumerable<AssetsDTO>> GetFixedByAssetId(int assetId)
        {
            var assetsFixed = await assetsRepository.GetFixedByAssetId(assetId);
            return mapper.Map<IEnumerable<AssetsDTO>>(assetsFixed);
        }
        public async Task<IEnumerable<AssetsDTO>> GetInternacionalAssetsByAssetId(int assetId)
        {
            var internacionalAssets = await assetsRepository.GetInternacionalAssetsByAssetId(assetId);
            return mapper.Map<IEnumerable<AssetsDTO>>(internacionalAssets);
        }
        public async Task<IEnumerable<UserAssetsDTO>> GetUserAssetsByAssetId(int assetId)
        {
            var userAssets = await assetsRepository.GetUserAssetsByAssetId(assetId);
            return mapper.Map<IEnumerable<UserAssetsDTO>>(assetId);
        }

        public async Task<IEnumerable<AssetsDTO>> GetAllByIdsAsync(IEnumerable<int>? entitysDTO)
        {
            var entitys = await assetsRepository.GetAllByIdsAsync(entitysDTO);
            return mapper.Map<IEnumerable<AssetsDTO>>(entitys);
        }
    }
}