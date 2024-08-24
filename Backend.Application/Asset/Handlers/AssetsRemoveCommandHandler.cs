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
    public class AssetsRemoveCommandHandler : IRequestHandler<AssetsRemoveCommand, Assets>
    {
        private readonly IAssetsRepository assetsRepository;
        public AssetsRemoveCommandHandler(IAssetsRepository assetsRepository) 
        {
            this.assetsRepository = assetsRepository;
        }
        public async Task<Assets> Handle(AssetsRemoveCommand request, CancellationToken cancellationToken)
        {
            var asset = new Assets(request.Id, request.CodName, request.CurrentPrice, request.SourceTypeAssets, request.SourceCreate, request.Deleted_at, request.Created_at, request.Updated_at);
            if(asset == null)
                throw new Exception("Erro when assest was being deleted ");

            return await assetsRepository.DeleteAsync(asset);
        }
    }
}