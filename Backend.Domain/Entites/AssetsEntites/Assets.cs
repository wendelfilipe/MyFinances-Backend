using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Validation;

namespace Backend.Domain.Entites.AssetsEntites
{
    public class Assets : AssetsEntity
    {
        public Wallet Wallet { get; private set; }

        public Assets(string codName, decimal currentPrice, decimal buyPrice, long amount)
        {
            DomainExceptionValidation.When(currentPrice <= 0.0m,
                "Invalid CurrentPrice, invalid value");
            DomainExceptionValidation.When(buyPrice <= 0.0m,
                "Invalid BuyPrice, invalid value");
            DomainExceptionValidation.When(amount < 0,
                 "Invalid amount, invalid value");
            ValidateDomain(codName);

            CodName = codName;
            CurrentPrice = currentPrice;
            BuyPrice = buyPrice;
            Amount = amount;
        }
    }
}