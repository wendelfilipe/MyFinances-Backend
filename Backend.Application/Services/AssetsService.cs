using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Application.Services.EntityServices;
using Backend.Domain.Entites.AssetsEntites;
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

    }
}