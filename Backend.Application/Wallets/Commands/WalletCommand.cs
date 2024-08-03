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
        public string Name { get; set; }
        public int UserId { get; set; }
        public SourceCreate SourceCreate { get; set; }
        public DateTime? Deleted_at { get; set; } = null;
        public DateTime Created_at { get; set; } = DateTime.UtcNow;
        public DateTime Updated_at { get; set; } = DateTime.UtcNow;
    }
}