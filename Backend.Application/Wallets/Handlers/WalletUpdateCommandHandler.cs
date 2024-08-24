using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.Wallets.Commands;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Interfaces.WalletInterface;
using MediatR;

namespace Backend.Application.Wallets.Handlers
{
    public class WalletUpdateCommandHandler : IRequestHandler<WalletUpdateCommand, Wallet>
    {
        private readonly IWalletRepository walletRepository;
        public WalletUpdateCommandHandler(IWalletRepository walletRepository)
        {
            this.walletRepository = walletRepository;
        }
        public async Task<Wallet> Handle(WalletUpdateCommand request, CancellationToken cancellationToken)
        {
            var wallet = await walletRepository.GetByIdAsync(request.Id);
            if(wallet == null)
                throw new Exception("wallet not found, when tried update");

            wallet.Update(request.Id, request.Name, request.SourceCreate, request.Deleted_at, request.Created_at, request.Updated_at);

            return await walletRepository.UpdateAsync(wallet);
        }
    }
}