using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Validation;

namespace Backend.Domain.Entites.WalletEntites
{
    public sealed class Wallet : Entity
    {
        public string UserId { get; private set; }
        public string Name { get; private set; }

        public ICollection<Assets> Assets { get; private set; }
        public Wallet(string name, string userId, SourceCreate sourceCreate, DateTime? deleted_at, DateTime created_at, DateTime updated_at)
        {
            ValidateDomain(name);
            UserId = userId;
            Name = name;
            SourceCreate = sourceCreate;
            Deleted_at = deleted_at;
            Created_at = created_at;
            Updated_at = updated_at;
        }

        public void Update(int id, string name, SourceCreate sourceCreate, DateTime? deleted_at, DateTime created_at, DateTime updated_at)
        {
            ValidateDomain(name);
            Id = id;
            Name = name;
            SourceCreate = sourceCreate;
            Deleted_at = deleted_at;
            Created_at = created_at;
            Updated_at = updated_at;
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name, Name is required");

            Name = name;
        }


    }
}