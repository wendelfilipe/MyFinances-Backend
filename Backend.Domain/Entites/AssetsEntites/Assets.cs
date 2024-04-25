using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Validation;

namespace Backend.Domain.Entites.AssetsEntites
{
    public class Assets : AssetsEntity
    {
        public Assets(string codName, decimal currentPrice, decimal buyPrice)
        {
            DomainExceptionValidation.When(currentPrice <= 0.0m,
                "Invalid CurrentPrice, invalid value");
            DomainExceptionValidation.When(buyPrice <= 0.0m,
                "Invalid BuyPrice, invalid value");
            ValidateDomain(codName);
        }
    }
}