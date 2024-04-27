using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;

namespace Backend.Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required")]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The email is required")]
        [MaxLength(100)]
        [MinLength(5)]
        public  string Email { get; set; } 

        [Required(ErrorMessage = "The password is required")]
        [MaxLength(100)]
        [MinLength(8)]
        public string Password { get; set; }
        
        public SourceCreate SourceCreate { get; set; }
        public DateTime Deleted_at { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}