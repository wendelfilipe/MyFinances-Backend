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
    public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, Assets>
    {
        private readonly IAssetsRepository assetsRepository;
        public GetAssetByIdQueryHandler(IAssetsRepository assetsRepository)
        {
            this.assetsRepository = assetsRepository;
        }
        public async Task<Assets> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {
            return await assetsRepository.GetByIdAsync(request.Id);
        }
    }
}