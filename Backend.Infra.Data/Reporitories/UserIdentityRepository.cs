using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Domain.Interfaces;
using Backend.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace Backend.Infra.Data.Reporitories
{
    public class UserIdentityRepository : IUserIdentityRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UserIdentityRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<string> GetUserIdAsync(ClaimsPrincipal user)
        {
            var userIdentity = await userManager.GetUserAsync(user);
            if(userIdentity == null)
                throw new Exception("Not found, erro when was looking for user identity");

            return userIdentity.Id;
        }
    }
}