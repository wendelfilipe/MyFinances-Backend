using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Validation;

namespace Backend.Domain.Entites
{
    public abstract class AssetsEntity
    {
        public int Id { get; private set; }
        public int WalletId {get; private set; }
        public string CodName { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public decimal BuyPrice { get; private  set; }
        public decimal AveregePrice { get; private set; } = 0;
        public SourceAssets Source { get; private set; }
        public DateTime Deleted_at { get; private set; }
        public DateTime Created_at { get; protected set; }
        public DateTime Updated_at { get; protected set; }

         public void ValidateDomain(string codName)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(codName),
             "CodName is Required");
            DomainExceptionValidation.When(codName.Length < 5,
             "CodName is Invalid, CodName is short");

            CodName = codName;

            
        }

        public void Update(int walletId, string codName, decimal currentPrice, decimal buyPrice)
        {
            DomainExceptionValidation.When(walletId < 1,
                "Invalid User Id value");
            DomainExceptionValidation.When(currentPrice <= 0 || buyPrice <= 0,
                "Invalid Current Price or Buy Price, invalid value");
                ValidateDomain(codName);

            WalletId = walletId;
            CodName = codName;
            CurrentPrice = currentPrice;
            BuyPrice = buyPrice;
        }
    }
    
}