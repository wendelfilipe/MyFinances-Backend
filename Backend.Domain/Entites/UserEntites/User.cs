using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            string gmailPattern = @"@gmail\.com$";
            string hotmailPattern = @"@hotmail\.com$";

            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name, Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minumum 3 caracters");

            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Invalid email, Email is required");
            DomainExceptionValidation.When(email.Length < 5, "Invalid name, too short, minumum 3 caracters");

            DomainExceptionValidation.When(!(Regex.IsMatch(email, pattern) 
                && Regex.IsMatch(email, gmailPattern))
                &&!(Regex.IsMatch(email, pattern) 
                && Regex.IsMatch(email, hotmailPattern))
                && email != "admin@admin",
                "Invalid Email, Valid Email is @gmail.com or @hotmal.com");

            DomainExceptionValidation.When(string.IsNullOrEmpty(password), "Invalid password, Password is required");
            DomainExceptionValidation.When(password.Length < 8, "Invalid password, too short, minumum 8 caracters");

            Name = name;
            Email = email;
            Password = password;
        }

    }
}