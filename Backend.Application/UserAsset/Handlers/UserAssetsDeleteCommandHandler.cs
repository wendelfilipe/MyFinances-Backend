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
    public class UserAssetsDeleteCommandHandler : IRequestHandler<UserAssetsDeleteCommand, UserAssets>
    {
        private readonly IUserAssetsRepository userAssetsRepository;
        public UserAssetsDeleteCommandHandler(IUserAssetsRepository userAssetsRepository)
        {
            this.userAssetsRepository = userAssetsRepository;
        }
        public async Task<UserAssets> Handle(UserAssetsDeleteCommand request, CancellationToken cancellationToken)
        {
            var userAsset = await userAssetsRepository.GetByIdAsync(request.Id);
            if(userAsset == null)
                throw new Exception("UserAsset not found, erro when being got UserAsset on Handler");

            return await userAssetsRepository.DeleteAsync(userAsset);
        }
    }
}