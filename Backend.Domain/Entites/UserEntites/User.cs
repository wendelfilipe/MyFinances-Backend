using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Validation;

namespace Backend.Domain.Entites.UserEntites
{
    // sealed -> não pode ser herdada
    public sealed class User : Entity
    {
        // private set -> só pode ser alterado
        public int? WalletId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public ICollection<Wallet> Wallet { get; private set; }

        public User(string name, string email, string password)
        {
            ValidateDomain(name, email, password);
        }

        private void ValidateDomain(string name, string email, string password)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "invalid name, Name is required");
            DomainExceptionValidation.When(name.Length < 3, "invalid name, too short, minumum 3 caracters");

            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "invalid email, Email is required");
            DomainExceptionValidation.When(email.Length < 3, "invalid name, too short, minumum 3 caracters");

            DomainExceptionValidation.When(string.IsNullOrEmpty(password), "invalid password, Name is required");
            DomainExceptionValidation.When(name.Length < 3, "invalid name, too password, minumum 3 caracters");
        }

    }
}