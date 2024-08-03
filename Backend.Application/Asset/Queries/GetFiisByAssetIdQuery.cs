using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using MediatR;

namespace Backend.Application.Asset.Queries
{
    public class GetFiisByAssetIdQuery : IRequest<IEnumerable<Assets>>
    {
        public int AssetId { get; set; }
        public GetFiisByAssetIdQuery(int assetId)
        {
            AssetId = assetId;
        }
    }
}