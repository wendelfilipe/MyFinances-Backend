using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.Wallets.Queries;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Interfaces.WalletInterface;
using MediatR;

namespace Backend.Application.Wallets.Handlers
{
    public class GetAllWalletsDTOByUserIdQueryHandler : IRequestHandler<GetAllWalletsDTOByUserIdQuery, IEnumerable<Wallet>>
    {
        private readonly IWalletRepository walletRepository;
        public GetAllWalletsDTOByUserIdQueryHandler(IWalletRepository walletRepository) 
        {
            this.walletRepository = walletRepository;
        }
        public async Task<IEnumerable<Wallet>> Handle(GetAllWalletsDTOByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await walletRepository.GetAllWalletsByUserId(request.UserId);
        }
    }
}