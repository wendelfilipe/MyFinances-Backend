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
    public class UserAssetsUpdateCommandHandler : IRequestHandler<UserAssetsUpdateCommand, UserAssets>
    {
        private readonly IUserAssetsRepository userAssetsRepository;
        public UserAssetsUpdateCommandHandler(IUserAssetsRepository userAssetsRepository)
        {
            this.userAssetsRepository = userAssetsRepository;
        }
        public async Task<UserAssets> Handle(UserAssetsUpdateCommand request, CancellationToken cancellationToken)
        {
            var userAsset = await userAssetsRepository.GetByIdAsync(request.Id);
            if(userAsset == null)
                throw new Exception("userAsset not found, erro when being got ");

            userAsset.Update(
                request.Id,
                request.WalletId,
                request.AssetsId,
                request.Amount,
                request.BuyPrice,
                request.StartDate,
                request.SourceTypeAssets,
                request.SourceCreate
            );

            return await userAssetsRepository.UpdateAsync(userAsset);
        }
    }
}