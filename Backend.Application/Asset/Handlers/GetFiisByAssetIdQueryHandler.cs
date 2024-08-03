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
    public class GetFiisByAssetIdQueryHandler : IRequestHandler<GetFiisByAssetIdQuery, IEnumerable<Assets>>
    {
        private readonly IAssetsRepository assetsRepository;
        public GetFiisByAssetIdQueryHandler(IAssetsRepository assetsRepository)
        {
            this.assetsRepository = assetsRepository;
        }
        public async Task<IEnumerable<Assets>> Handle(GetFiisByAssetIdQuery request, CancellationToken cancellationToken)
        {
            return await assetsRepository.GetFiisByAssetId(request.AssetId);
        }
    }
}