using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.UserEntites;
using MediatR;

namespace Backend.Application.Users.Queries
{
    public class GetAllUserQuery : IRequest<IEnumerable<User>
    {
        
    }
}