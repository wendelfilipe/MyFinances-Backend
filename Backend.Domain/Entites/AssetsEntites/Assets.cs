using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.UserAssetsEntity;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Validation;

namespace Backend.Domain.Entites.AssetsEntites
{
    public class Assets : AssetsEntity
    {
        public ICollection<Wallet> Wallets { get; private set; }

        public Assets(string codName, decimal currentPrice)
        {
            DomainExceptionValidation.When(currentPrice <= 0.0m,
                "Invalid CurrentPrice, invalid value");
            ValidateDomain(codName);

            CodName = codName;
            CurrentPrice = currentPrice;
        }
    }
}