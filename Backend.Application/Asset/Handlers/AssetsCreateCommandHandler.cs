using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.Asset.Commands;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Interfaces.AssetsInterface;
using MediatR;

namespace Backend.Application.Asset.Handlers
{
    public class AssetsCreateCommandHandler : IRequestHandler<AssetsCreateCommand, Assets>
    {
        private readonly IAssetsRepository assetsRepository;
        public AssetsCreateCommandHandler(IAssetsRepository assetsRepository)
        {
            this.assetsRepository = assetsRepository;
        }
        public async Task<Assets> Handle(AssetsCreateCommand request, CancellationToken cancellationToken)
        {
            var asset = new Assets(request.CodName, request.CurrentPrice, request.SourceTypeAssets, request.SourceCreate, request.Deleted_at, request.Created_at, request.Updated_at);
            if(asset == null )
                throw new Exception("Erro when create asset");

            return await assetsRepository.CreateAsync(asset);
        }
    }

}