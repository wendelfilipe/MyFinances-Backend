using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Entites.UserEntites;
using MediatR;

namespace Backend.Application.Users.Commands
{
    public class UserCommand : IRequest<User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public SourceCreate SourceCreate { get; set; }
        public DateTime? Deleted_at { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}