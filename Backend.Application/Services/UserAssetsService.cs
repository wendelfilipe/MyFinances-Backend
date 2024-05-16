using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Application.Services.EntityServices;
using Backend.Domain.Entites.UserAssetsEntity;
using Backend.Domain.Interfaces;
using Backend.Domain.Interfaces.UserInterface;

namespace Backend.Application.Services
{
    public class UserAssetsService : EntityService<UserAssets, UserAssetsDTO>, IUserAssetsService
    {
        private readonly IUserAssetsRepository userAssetsRepository;
        private readonly IMapper mapper;
        public UserAssetsService(IEntityRepository<UserAssets> entityRepository, IMapper mapper, IUserAssetsRepository userAssetsRepository) : base(entityRepository, mapper)
        {
            this.userAssetsRepository = userAssetsRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserAssetsDTO>> GetAllUserAssetsByWalletId(int walletId)
        {
            var userAssets = await userAssetsRepository.GetAllUserAssetsByWalletId(walletId);
            return mapper.Map<IEnumerable<UserAssetsDTO>>(userAssets);
        }
    }
}