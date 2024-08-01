using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Entites.WalletEntites;
using MediatR;

namespace Backend.API.Wallets.Commands
{
    public class WalletCommand : IRequest<Wallet>
    {
        public string Name { get; private set; }
        public int UserId { get; private set; }
        public SourceCreate SourceCreate { get; protected set; }
        public DateTime? Deleted_at { get; protected set; } = null;
        public DateTime Created_at { get; protected set; } = DateTime.UtcNow;
        public DateTime Updated_at { get; protected set; } = DateTime.UtcNow;
    }
}