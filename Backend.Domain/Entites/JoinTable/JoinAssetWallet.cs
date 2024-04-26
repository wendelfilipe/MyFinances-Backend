using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Entites.WalletEntites;

namespace Backend.Domain.Entites
{
    public class JoinAssetWallet
    {
        public int WalletId { get; private set; }

        public Wallet Wallet { get; private set; }
        public int AssetId { get; private set; }
        public ICollection<Assets> Assets { get; private set; }
    }
}