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
        public int Id { get; protected set; }
        public int WalletId {get; protected set; }
        public string CodName { get; protected set; }
        public decimal CurrentPrice { get; protected set; }
        public decimal BuyPrice { get; protected  set; }
        public SourceTypeAssets SourceTypeAssets {get; protected set; }
        public decimal AveregePrice { get; protected set; } = 0;
        public SourceCreate SourceCreate { get; protected set; }
        public DateTime Deleted_at { get; protected set; }
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

        public void Update( string codName, decimal currentPrice, decimal buyPrice)
        {
            DomainExceptionValidation.When(currentPrice <= 0.0m,
                "Invalid CurrentPrice, invalid value");
            DomainExceptionValidation.When(buyPrice <= 0.0m,
                "Invalid BuyPrice, invalid value");
            ValidateDomain(codName);
            
            CodName = codName;
            CurrentPrice = currentPrice;
            BuyPrice = buyPrice;
        }
    }
    
}