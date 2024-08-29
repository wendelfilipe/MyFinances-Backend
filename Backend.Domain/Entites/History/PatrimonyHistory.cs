using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Validation;

namespace Backend.Domain.Entites.History
{
    public class PatrimonyHistory : Entity
    {
        public string UserId { get; private set; }
        public decimal Patrimony { get; private set; }
        public DateOnly Day { get; private set; }

        public PatrimonyHistory(int id, string userId, decimal patrimony, DateOnly day, SourceCreate sourceCreate, DateTime? deleted_at, DateTime created_at, DateTime updated_at    
        )
        {
            DomainExceptionValidation.When(id <= 0, "Invalid id, id must be biger then 0");
            ValidateDomain(userId, patrimony);
            Id = id;
            Day = day;
            SourceCreate = sourceCreate;
            Deleted_at = deleted_at;
            Created_at = created_at;
            Updated_at = updated_at;
        }

        public PatrimonyHistory(string userId, decimal patrimony, DateOnly day, SourceCreate sourceCreate, DateTime? deleted_at, DateTime created_at, DateTime updated_at    
        )
        {
            ValidateDomain(userId, patrimony);
            Day = day;
            SourceCreate = sourceCreate;
            Deleted_at = deleted_at;
            Created_at = created_at;
            Updated_at = updated_at;
        }

        public void Update(int id, string userId, decimal patrimony, DateOnly day, SourceCreate sourceCreate, DateTime? deleted_at, DateTime created_at, DateTime updated_at    
        )
        {
            ValidateDomain(userId, patrimony);
            Id = id;
            Day = day;
            SourceCreate = sourceCreate;
            Deleted_at = deleted_at;
            Created_at = created_at;
            Updated_at = updated_at;
        }

        private void ValidateDomain(string userId, decimal patrimony)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(userId), "Invalid userId, userId is null or empty");
            DomainExceptionValidation.When(patrimony < 0, "Invalid Patrimony, Patrimony must be equals or biger then 0");
            UserId = userId;
            Patrimony = patrimony;
        }
    }
}