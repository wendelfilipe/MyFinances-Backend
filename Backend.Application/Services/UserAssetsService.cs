using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.Asset.Queries;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Application.UserAsset.Command;
using Backend.Domain.Entites.UserAssetsEntity;
using Backend.Domain.Interfaces;
using Backend.Domain.Interfaces.UserInterface;
using MediatR;

namespace Backend.Application.Services
{
    public class UserAssetsService : IUserAssetsService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        public UserAssetsService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task CreateUserAssetsAsync(UserAssetsDTO userAssetsDTO)
        {
            var userAssetsCreateCommand = mapper.Map<UserAssetsCreateCommand>(userAssetsDTO);
            if(userAssetsCreateCommand == null)
                throw new Exception("UserAssetCreateCommand not found");
            
            await mediator.Send(userAssetsCreateCommand);
        }

        public async Task DeleteUserAssetAsync(int id)
        {
            var userAssetsDeleteCommand = new UserAssetsDeleteCommand(id);
            if(userAssetsDeleteCommand == null)
                throw new Exception("UserAssetDeleteCommand not found");

            await mediator.Send(userAssetsDeleteCommand);
        }

        public async Task<IEnumerable<UserAssetsDTO>> GetAllUserAssetsByWalletId(int walletId)
        {
            var getAllUserAssetsByWalletId = new GetAllAssetsDTOByAssetIdQuery(walletId);
            if(getAllUserAssetsByWalletId ==  null)
                throw new Exception("UserAsset not found, erro when being got UserAsset by wallet id on service");

            var result = await mediator.Send(getAllUserAssetsByWalletId);

            return mapper.Map<IEnumerable<UserAssetsDTO>>(result);
            
        }

        public async Task UpdateUserAssetsAsync(UserAssetsDTO userAssetsDTO)
        {
            var userAssetsUpdateCommand = mapper.Map<UserAssetsUpdateCommand>(userAssetsDTO);
            if(userAssetsUpdateCommand == null)
                throw new Exception("UserAsset not found, erro when was being updated on service");

            await mediator.Send(userAssetsUpdateCommand);
        }
    }
}