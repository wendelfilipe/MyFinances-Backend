using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces
{
    public interface IUserIdentityService
    {
        Task<string> GetUserId(ClaimsPrincipal user);
    }
}