using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Validation;

namespace Backend.Domain.Entites.WalletEntites
{
    public sealed class Wallet : Entity
    {
        public int UserId { get; private set; }
        public string Name { get; private set; }

        public Wallet(string name)
        {
            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(UserId < 0, "Invalid UserId value ");

            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "invalid name, Name is required");
            DomainExceptionValidation.When(name.Length < 1, "invalid name, too short, minumum 1 caracters");
        }


    }
}