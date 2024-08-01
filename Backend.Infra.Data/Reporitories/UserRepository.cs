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
    public class UserRepository : EntityRepository<User>, IUserRepository
    {
        private readonly AppDbContext context;
        public UserRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await context.User.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}