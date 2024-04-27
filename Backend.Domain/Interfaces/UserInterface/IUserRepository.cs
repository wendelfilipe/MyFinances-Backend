using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.UserEntites;

namespace Backend.Domain.Interfaces.UserInterface
{
    public interface IUserRepository : IEntityRepository<User>
    {
    }
}