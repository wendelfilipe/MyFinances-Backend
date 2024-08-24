using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Entites.UserAssetsEntity;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Validation;

namespace Backend.Domain.Entites.AssetsEntites
{
    public class Assets : AssetsEntity
    {
        public Assets(int id, string codName, decimal currentPrice, SourceTypeAssets sourceTypeAssets, SourceCreate sourceCreate, DateTime? deleted_at, DateTime created_at, DateTime updated_at)
        {
            DomainExceptionValidation.When(currentPrice <= 0.0m,
                "Invalid CurrentPrice, invalid value");
            ValidateDomain(codName);

            CodName = codName;
            CurrentPrice = currentPrice;
            Id = id;
            SourceTypeAssets = sourceTypeAssets;
            SourceCreate = sourceCreate;
            Deleted_at = deleted_at;
            Created_at = created_at;
            Updated_at = updated_at;
        }

        public Assets(string codName, decimal currentPrice, SourceTypeAssets sourceTypeAssets, SourceCreate sourceCreate, DateTime? deleted_at, DateTime created_at, DateTime updated_at)
        {
            DomainExceptionValidation.When(currentPrice <= 0.0m,
                "Invalid CurrentPrice, invalid value");
            ValidateDomain(codName);

            CodName = codName;
            CurrentPrice = currentPrice;
            SourceTypeAssets = sourceTypeAssets;
            SourceCreate = sourceCreate;
            Deleted_at = deleted_at;
            Created_at = created_at;
            Updated_at = updated_at;
        }
        
         public void Update(int id, string codName, decimal currentPrice, SourceTypeAssets sourceTypeAssets, SourceCreate sourceCreate, DateTime? deleted_at, DateTime created_at, DateTime updated_at)
        {
            DomainExceptionValidation.When(currentPrice <= 0.0m,
                "Invalid CurrentPrice, invalid value");
            ValidateDomain(codName);

            CodName = codName;
            CurrentPrice = currentPrice;
            Id = id;
            SourceTypeAssets = sourceTypeAssets;
            SourceCreate = sourceCreate;
            Deleted_at = deleted_at;
            Created_at = created_at;
            Updated_at = updated_at;
        }
    }
}