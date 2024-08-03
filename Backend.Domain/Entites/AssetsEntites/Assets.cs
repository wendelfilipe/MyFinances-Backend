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
        public Assets(int id, string codName, decimal currentPrice)
        {
            DomainExceptionValidation.When(currentPrice <= 0.0m,
                "Invalid CurrentPrice, invalid value");
            ValidateDomain(codName);

            CodName = codName;
            CurrentPrice = currentPrice;
            Id = id;
        }

        public Assets(string codName, decimal currentPrice)
        {
            DomainExceptionValidation.When(currentPrice <= 0.0m,
                "Invalid CurrentPrice, invalid value");
            ValidateDomain(codName);

            CodName = codName;
            CurrentPrice = currentPrice;
        }
        
         public void Update(int id, string codName, decimal currentPrice)
        {
            DomainExceptionValidation.When(currentPrice <= 0.0m,
                "Invalid CurrentPrice, invalid value");
            ValidateDomain(codName);

            CodName = codName;
            CurrentPrice = currentPrice;
            Id = id;
        }
    }
}