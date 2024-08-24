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
            try
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    throw new Exception("UserIdClaim not found");
                
                return userIdClaim.Value;
                
                // var userIdentity = await userManager.GetUserAsync(user);
                // if (userIdentity == null)
                //     throw new Exception("UserIdClaim not found");
                //
                // return userIdentity.Id;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}