using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.Interfaces;
using Backend.Domain.Interfaces;
using MediatR;

namespace Backend.Application.Services
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IUserIdentityRepository userIdentityRepository;
        public UserIdentityService(IUserIdentityRepository userIdentityRepository)
        {
            this.userIdentityRepository = userIdentityRepository;
        }
       
        public async Task<string> GetUserId(ClaimsPrincipal user)
        {
            return await userIdentityRepository.GetUserIdAsync(user);
        }
    }
}