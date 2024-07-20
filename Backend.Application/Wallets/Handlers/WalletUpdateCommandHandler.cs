using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.Wallets.Commands;
using Backend.Domain.Entites.WalletEntites;
using MediatR;

namespace Backend.Application.Wallets.Handlers
{
    public class WalletUpdateCommandHandler : IRequestHandler<WalletUpdateCommand, Wallet>
    {
        
    }
}