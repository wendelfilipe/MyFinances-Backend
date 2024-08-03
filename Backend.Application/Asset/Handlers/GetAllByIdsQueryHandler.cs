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
    public class GetAllByIdsQueryHandler : IRequestHandler<GetAllByIdsQuery, IEnumerable<Assets>>
    {
        private readonly IAssetsRepository assetsRepository;
        public GetAllByIdsQueryHandler(IAssetsRepository assetsRepository)
        {
            this.assetsRepository = assetsRepository;
        }
        public async Task<IEnumerable<Assets>> Handle(GetAllByIdsQuery request, CancellationToken cancellationToken)
        {
            return await assetsRepository.GetAllByIdsAsync(request.Ids);
        }
    }
}