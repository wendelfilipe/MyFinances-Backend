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

            wallet.Update(request.Name);

            return await walletRepository.UpdateAsync(wallet);
        }
    }
}