using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.Asset.Commands;
using Backend.Application.Asset.Queries;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
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

        public async Task CreateAssetAsync(AssetsDTO assetsDTO)
        {
           var assetCreateCommand = mapper.Map<AssetsCreateCommand>(assetsDTO);
           if(assetCreateCommand == null)
                throw new Exception("Asset not found, erro when being creating assets");

           await mediator.Send(assetCreateCommand);
        }

        public async Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOByAssetIdAsync(int assetId)
        {
            var getAllAssetsDTOByAssetId = new GetAllAssetsDTOByAssetIdQuery(assetId);
            if(getAllAssetsDTOByAssetId == null)
                throw new Exception("Erro when being got All Assets by AssetId");
            
            var result = await mediator.Send(getAllAssetsDTOByAssetId);

            return mapper.Map<IEnumerable<AssetsDTO>>(result);
        }

        public async Task<IEnumerable<AssetsDTO>> GetAllAssetsAsync()
        {
            var getAllAssets = new GetAllAssetsQuery();
            if(getAllAssets == null)
                throw new Exception("Erro when gotting All Assets");

            var result = await mediator.Send(getAllAssets);

            return mapper.Map<IEnumerable<AssetsDTO>>(result);
        }

        public async Task<IEnumerable<AssetsDTO>> GetAllByIdsAsync(IEnumerable<int>? assetsDTO)
        {
            var getAllByIds = new GetAllByIdsQuery(assetsDTO);
            if(getAllByIds == null)
                throw new Exception("Erro when being got All Assets by Ids");
            
            var result = await mediator.Send(getAllByIds);

            return mapper.Map<IEnumerable<AssetsDTO>>(result);
        }

        public async Task<AssetsDTO> GetAssetByIdAsync(int id)
        {
            var getAssetById = new GetAssetByIdQuery(id);
            if(getAssetById == null)
                throw new Exception("Erro when gotting assets by id");
            
            var result = await mediator.Send(getAssetById);

            return mapper.Map<AssetsDTO>(result);
        }

        public async Task<IEnumerable<AssetsDTO>> GetFiisByAssetIdAsync(int assetId)
        {
            var getFiisByAssetId = new GetFiisByAssetIdQuery(assetId);
            if(getFiisByAssetId == null)
                throw new Exception("Erro when being got Fiis by AssetId");

            var result = await mediator.Send(getFiisByAssetId);

            return mapper.Map<IEnumerable<AssetsDTO>>(result);
        }

        public async Task<IEnumerable<AssetsDTO>> GetFixedByAssetIdAsync(int assetId)
        {
            var getFixedByAssetId = new GetFixedByAssetIdQuery(assetId);
            if(getFixedByAssetId == null)
                throw new Exception("Erro when being got Fixed by AssetId");

            var result = await mediator.Send(getFixedByAssetId);

            return mapper.Map<IEnumerable<AssetsDTO>>(result);
        }

        public async Task<IEnumerable<AssetsDTO>> GetInternacionalAssetsByAssetIdAsync(int assetId)
        {
            var getIntAssetByAssetId = new GetInternacionalAssetsByAssetIdQuery(assetId);
            if(getIntAssetByAssetId == null)
                throw new Exception("Erro when being got internacional Assets by assetId");
            
            var result = await mediator.Send(getIntAssetByAssetId);

            return mapper.Map<IEnumerable<AssetsDTO>>(result);
        }

        public async Task<IEnumerable<AssetsDTO>> GetStocksByAssetIdAsync(int assetId)
        {
            var getStocksByAssetId = new GetStocksByAssetIdQuery(assetId);
            if(getStocksByAssetId == null)
                throw new Exception("Error when being got stocks by assetId");

            var result = await mediator.Send(getStocksByAssetId);

            return mapper.Map<IEnumerable<AssetsDTO>>(result);
        }

        public async Task UpdateAssetAsync(AssetsDTO assetsDTO)
        {
            var assetsUpdateCommand = mapper.Map<AssetsUpdateCommand>(assetsDTO);
            if(assetsUpdateCommand == null)
                throw new Exception("Asset not found, erro when was being updated");

            await mediator.Send(assetsUpdateCommand);
        }
    }
}