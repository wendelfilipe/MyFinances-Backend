using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;

namespace Backend.Domain.Entites.UserAssetsEntity
{
    public class UserAssets : Entity
    {
        public int WalletId { get; private set; }
        public int AssetsId { get; private set; }
        public decimal? PerCentCDI { get; private set; }
        public long Amount { get; private set; }
        public decimal BuyPrice { get; private  set; }
        public decimal AveregePrice { get; private set; }
        public SourceTypeAssets SourceTypeAssets {get; protected set; }

    }
}