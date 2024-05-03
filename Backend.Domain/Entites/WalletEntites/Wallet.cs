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
        public int UserId { get; private set; }
        public string Name { get; private set; }

        public ICollection<Assets> Assets { get; private set; }

        public Wallet(string name)
        {
            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name, Name is required");

            Name = name;
        }


    }
}