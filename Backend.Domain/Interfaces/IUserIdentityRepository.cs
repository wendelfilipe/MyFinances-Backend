using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
    public interface IUserIdentityRepository
    {
        Task<string> GetUserIdAsync(ClaimsPrincipal user);
    }
}