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
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserController(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        
        [HttpGet("{email}", Name = "Get")]
        public async Task<UserDTO> Get(string email)
        {
            var userDTO = await userService.GetUserDTOByEmailAsync(email);

            if(userDTO == null)
            {
               throw new NullReferenceException("User is invalid");
            }
            //criar cookie de login
            string userIdJson = JsonSerializer.Serialize(userDTO.Id);
            Response.Cookies.Append("UserIdCookie",userIdJson,
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(7)
                });
            
            return userDTO;
            
        }

        [HttpPost(Name = "PostClickedOnLogOut")]
        public async Task PostClickedOnLogOutAsync()
        {
            var httpContext = httpContextAccessor.HttpContext;
            if(httpContext.Request.Cookies.ContainsKey("UserIdCookie"))
            {
                await Task.Run(() => httpContext.Response.Cookies.Delete("UserIdCookie"));
            }
        }
        [HttpPost(Name = "PostCreateUserByWebAsync")]
        public async Task PostCreateUserByWebAsync(UserDTO userDTO)
        {
            userDTO.Created_at = DateTime.Now;
            userDTO.Updated_at = DateTime.Now;
            userDTO.SourceCreate = SourceCreate.Web;
            userDTO.Deleted_at = null;

            await userService.CreateAsync(userDTO);
        }
    }
}