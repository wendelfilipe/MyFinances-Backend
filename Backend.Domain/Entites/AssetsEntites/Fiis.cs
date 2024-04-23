using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Validation;

namespace Backend.Domain.Entites.AssetsEntites
{
    public class Fiis : AssetsEntity
    {
        public Fiis(int userId, string codName, decimal currentPrice, decimal buyPrice)
        {
            DomainExceptionValidation.When(userId < 1,
                "Invalid UserId value");
            DomainExceptionValidation.When(currentPrice <= 0 || buyPrice <= 0,
                "Invalid CurrentPrice or BuyPrice, invalid value");
            ValidateDomain(codName);
        }
    }
}