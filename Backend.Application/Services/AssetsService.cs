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

        public async Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOByWalletIdAsync(int walletId)
        {
            var assetsEntity = await assetsRepository.GetAllAssetsByWalletIdAsync(walletId);
            return mapper.Map<IEnumerable<AssetsDTO>>(assetsEntity);
        }
         public async Task<IEnumerable<AssetsDTO>> GetStocksByWalletId(int walletId)
        {
            var stocks = await assetsRepository.GetStocksByWalletId(walletId);
            return mapper.Map<IEnumerable<AssetsDTO>>(stocks);
        }
        public async Task<IEnumerable<AssetsDTO>> GetFiisByWalletId(int walletId)
        {
            var fiis = await assetsRepository.GetFiisByWalletId(walletId);
            return mapper.Map<IEnumerable<AssetsDTO>>(fiis);
        }
        public async Task<IEnumerable<AssetsDTO>> GetFixedByWalletId(int walletId)
        {
            var assetsFixed = await assetsRepository.GetFixedByWalletId(walletId);
            return mapper.Map<IEnumerable<AssetsDTO>>(assetsFixed);
        }
        public async Task<IEnumerable<AssetsDTO>> GetInternacionalAssetsByWalletId(int walletId)
        {
            var internacionalAssets = await assetsRepository.GetInternacionalAssetsByWalletId(walletId);
            return mapper.Map<IEnumerable<AssetsDTO>>(internacionalAssets);
        }


    }
}