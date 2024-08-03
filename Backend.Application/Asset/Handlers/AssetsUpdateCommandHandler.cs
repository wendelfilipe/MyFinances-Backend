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
    public class AssetsUpdateCommandHandler : IRequestHandler<AssetsUpdateCommand, Assets>
    {
        private readonly IAssetsRepository assetsRepository;
        public AssetsUpdateCommandHandler(IAssetsRepository assetsRepository)
        {
            this.assetsRepository = assetsRepository;
        }
        public async Task<Assets> Handle(AssetsUpdateCommand request, CancellationToken cancellationToken)
        {
            var asset = await assetsRepository.GetByIdAsync(request.Id);
            if(asset == null)
                throw new Exception("Erro when was being updated");

            asset.Update(request.Id, request.CodName, request.CurrentPrice);

            return await assetsRepository.UpdateAsync(asset);
        }
    }
}