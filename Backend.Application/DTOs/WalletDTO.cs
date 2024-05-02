using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Entites.UserEntites;

namespace Backend.Application.DTOs
{
    public class WalletDTO
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        [Required(ErrorMessage = "The name is required")]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        public SourceCreate SourceCreate { get; set; }
        public DateTime? Deleted_at { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}