using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.Asset.Queries;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Interfaces.AssetsInterface;
using MediatR;

namespace Backend.Application.Asset.Handlers
{
    public class GetFixedByAssetIdQueryHandler : IRequestHandler<GetFixedByAssetIdQuery, IEnumerable<Assets>>
    {
        private readonly IAssetsRepository assetsRepository;
        public GetFixedByAssetIdQueryHandler(IAssetsRepository assetsRepository)
        {
            this.assetsRepository = assetsRepository;
        }
        public async Task<IEnumerable<Assets>> Handle(GetFixedByAssetIdQuery request, CancellationToken cancellationToken)
        {
            return await assetsRepository.GetFixedByAssetId(request.AssetId);
        }
    }
}