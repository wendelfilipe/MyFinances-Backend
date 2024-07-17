using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.Users.Commands
{
    public class UserDeleteCommand
    {
        public int Id { get; set;}
        public UserDeleteCommand(int id)
        {
            Id = id;
        }
    }
}