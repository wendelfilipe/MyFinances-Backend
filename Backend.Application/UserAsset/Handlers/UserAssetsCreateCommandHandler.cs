using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.UserAsset.Command;
using Backend.Domain.Entites.UserAssetsEntity;
using Backend.Domain.Interfaces.UserInterface;
using MediatR;

namespace Backend.Application.UserAsset.Handlers
{
    public class UserAssetsCreateCommandHandler : IRequestHandler<UserAssetsCreateCommand, UserAssets>
    {
        private readonly IUserAssetsRepository userAssetsRepository;
        public UserAssetsCreateCommandHandler(IUserAssetsRepository userAssetsRepository)
        {
            this.userAssetsRepository = userAssetsRepository;
        }
        public async Task<UserAssets> Handle(
            UserAssetsCreateCommand request, 
            CancellationToken cancellationToken
        )
        {
            var userAsset = new UserAssets(
                request.WalletId, 
                request.AssetsId, 
                request.Amount, 
                request.BuyPrice, 
                request.StartDate, 
                request.SourceTypeAssets, 
                request.SourceCreate
            ); 

            if(userAsset == null)
                throw new Exception("UserAsset not found, erro on handler");

            return await userAssetsRepository.CreateAsync(userAsset);
        }
    }
}