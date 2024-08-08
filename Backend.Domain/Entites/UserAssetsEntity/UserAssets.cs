using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Entites.WalletEntites;

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
        public SourceTypeAssets SourceTypeAssets {get; private set; }
        public string StartDate { get; private set; }
        public string? EndDate { get; private set; }

         public UserAssets(int walletId, int assetsId, long amount, decimal buyPrice, string startDate, SourceTypeAssets sourceTypeAssets, SourceCreate sourceCreate)
        {
            WalletId = walletId;
            AssetsId = assetsId;
            Amount = amount;
            BuyPrice = buyPrice;
            StartDate = startDate;
            SourceCreate = sourceCreate;
            SourceTypeAssets = sourceTypeAssets;
        }

        public UserAssets(int id, int walletId, int assetsId, long amount, decimal buyPrice, string startDate, SourceTypeAssets sourceTypeAssets, SourceCreate sourceCreate)
        {
            Id = id;
            WalletId = walletId;
            AssetsId = assetsId;
            Amount = amount;
            BuyPrice = buyPrice;
            StartDate = startDate;
            SourceCreate = sourceCreate;
            SourceTypeAssets = sourceTypeAssets;
        }

        public void Update(int id, int walletId, int assetsId, long amount, decimal buyPrice, string startDate, SourceTypeAssets sourceTypeAssets, SourceCreate sourceCreate)
            {
                Id = id;
                WalletId = walletId;
                AssetsId = assetsId;
                Amount = amount;
                BuyPrice = buyPrice;
                StartDate = startDate;
                SourceCreate = sourceCreate;
                SourceTypeAssets = sourceTypeAssets;
            }
    }

}