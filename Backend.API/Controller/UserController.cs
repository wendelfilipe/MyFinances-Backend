using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Entites.WalletEntites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;

namespace Backend.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService; 
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.httpContextAccessor = httpContextAccessor;
        }

        
        [HttpGet("GetUserDTOByEmailAsync/{email}")]
        public async Task<UserDTO> GetUserDTOByEmailAsync(string email)
        {
            var userDTO = await userService.GetUserDTOByEmailAsync(email);

            if(userDTO == null)
            {
               throw new NullReferenceException("Email is invalid");
            }
            
            return userDTO;
            
        }

        [HttpGet("GetUserDTOByIdAsync/{userId}")]
        public async Task<UserDTO> GetUserDTOByIdAsync(int userId)
        {
            var userDTO = await userService.GetByIdAsync(userId);
            
            if(userDTO == null)
            {
                throw new Exception("Invalid User");
            }
            
            return userDTO;
            
        }

        [HttpPost("PostCreateUserByWebAsync")]
        public async Task PostCreateUserByWebAsync(UserDTO userDTO)
        {
            var user = await userService.GetUserDTOByEmailAsync(userDTO.Email);
            if(user != null)
                throw new Exception("Email ja exite");
                
            userDTO.Created_at = DateTime.UtcNow;
            userDTO.Updated_at = DateTime.UtcNow;
            userDTO.Deleted_at = null;

            await userService.CreateAsync(userDTO);
        }
    }
}