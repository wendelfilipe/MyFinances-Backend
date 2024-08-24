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
        public Wallet(string name, string userId)
        {
            ValidateDomain(name);
            UserId = userId;
            Name = name;
        }

        public void Update(string name)
        {
            ValidateDomain(name);
            Name = name;
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name, Name is required");

            Name = name;
        }


    }
}