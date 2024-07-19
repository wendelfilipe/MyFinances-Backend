using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.API.Wallets.Commands;

namespace Backend.Application.Wallets.Commands
{
    public class WalletRemoveCommand : WalletCommand
    {
        public int Id { get; set; }
        public WalletRemoveCommand(int id)
        {
            Id = id;
        }
    }
}