using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Interfaces.UserInterface;
using Backend.Infra.Data.Context;
using Backend.Infra.Data.Reporitories.EntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Data.EnititesConfiguration
{
    public class UserRepository : EntityRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}