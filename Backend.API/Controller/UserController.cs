using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.Interfaces;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Entites.WalletEntites;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService; 
        private readonly IMapper mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<User> Get(string email)
        {
            var user = await userService.GetUserDTOByEmailAsync(email);
            
            if(user == null)
            {
               throw new NullReferenceException("User is invalid");
            }

            string userIdJson = JsonSerializer.Serialize(user.Id);
            
            return mapper.Map<User>(user);
            
        }
    }
}