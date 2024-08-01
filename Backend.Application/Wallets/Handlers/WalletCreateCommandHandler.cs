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
    public class WalletCreateCommandHandler : IRequestHandler<WalletCreateCommand, Wallet>
    {
        private readonly IWalletRepository walletRepository;
        public WalletCreateCommandHandler(IWalletRepository walletRepository)
        {
            this.walletRepository = walletRepository;
        }
        public async Task<Wallet> Handle(
            WalletCreateCommand request, 
            CancellationToken cancellationToken
        )
        {
            var wallet = new Wallet(request.Name, request.UserId);

            if(wallet == null)
            {
                throw new ApplicationException("Error when create entity");
            }
            else
            {
                return await walletRepository.CreateAsync(wallet);
            }
        }
    }
}