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
using MediatR;

namespace Backend.Application.Services
{
    public class AssetsService : IAssetsService
    {
        private IMapper mapper;
        private IMediator mediator;
        public AssetsService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public Task CreateAsync(AssetsDTO entityDTO)
        {
            
        }

        public Task DeleteAsync(AssetsDTO entityDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOByAssetIdAsync(int assetId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssetsDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssetsDTO>> GetAllByIdsAsync(IEnumerable<int>? entitysDTO)
        {
            throw new NotImplementedException();
        }

        public Task<AssetsDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssetsDTO>> GetFiisByAssetId(int assetId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssetsDTO>> GetFixedByAssetId(int assetId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssetsDTO>> GetInternacionalAssetsByAssetId(int assetId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssetsDTO>> GetStocksByAssetId(int assetId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AssetsDTO entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}