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
    public class WalletRemoveCommandHandler : IRequestHandler<WalletRemoveCommand, Wallet>
    {
        private readonly IWalletRepository walletRepository;
        public WalletRemoveCommandHandler(IWalletRepository walletRepository)
        {
            this.walletRepository = walletRepository;
        }
        public async Task<Wallet> Handle(
            WalletRemoveCommand request, 
            CancellationToken cancellationToken
        )
        {
            var wallet = new Wallet(request.Name, request.UserId);

            if(wallet == null)
                throw new ArgumentException("Error when deleting entity");

            return await walletRepository.DeleteAsync(wallet);
        }
    }
}