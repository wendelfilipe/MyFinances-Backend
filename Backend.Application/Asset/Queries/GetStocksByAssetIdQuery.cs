using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using MediatR;

namespace Backend.Application.Asset.Queries
{
    public class GetStocksByAssetIdQuery : IRequest<IEnumerable<Assets>>
    {
        public int AssetsId { get; set; }
        public GetStocksByAssetIdQuery(int assetsId)
        {
            AssetsId = assetsId;            
        }
    }
}