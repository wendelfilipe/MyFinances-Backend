using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.WalletEntites;
using MediatR;

namespace Backend.Application.Wallets.Queries
{
    public class GetAllWalletsQuery : IRequest<IEnumerable<Wallet>>
    {
        
    }
}