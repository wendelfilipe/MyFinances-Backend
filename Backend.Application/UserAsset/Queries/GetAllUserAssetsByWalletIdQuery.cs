using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.UserAssetsEntity;
using MediatR;

namespace Backend.Application.UserAsset.Queries
{
    public class GetAllUserAssetsByWalletIdQuery : IRequest<IEnumerable<UserAssets>>
    {
        public int WalletId { get; set; }
        public GetAllUserAssetsByWalletIdQuery(int walletId)
        {
            WalletId = walletId;
        }
    }
}