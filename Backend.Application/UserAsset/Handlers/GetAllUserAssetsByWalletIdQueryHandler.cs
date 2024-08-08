using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.UserAsset.Queries;
using Backend.Domain.Entites.UserAssetsEntity;
using Backend.Domain.Interfaces.UserInterface;
using MediatR;

namespace Backend.Application.UserAsset.Handlers
{
    public class GetAllUserAssetsByWalletIdQueryHandler : IRequestHandler<GetAllUserAssetsByWalletIdQuery, IEnumerable<UserAssets>>
    {
        private readonly IUserAssetsRepository userAssetsRepository;
        public GetAllUserAssetsByWalletIdQueryHandler(IUserAssetsRepository userAssetsRepository)
        {
            this.userAssetsRepository = userAssetsRepository;
        }
        public async Task<IEnumerable<UserAssets>> Handle(GetAllUserAssetsByWalletIdQuery request, CancellationToken cancellationToken)
        {
            return await userAssetsRepository.GetAllUserAssetsByWalletId(request.WalletId);
        }
    }
}