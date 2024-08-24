using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Entites.WalletEntites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService walletService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        public WalletController(IWalletService walletService, IHttpContextAccessor httpContextAccessor, IUserService userService, IMapper mapper)
        {
            this.walletService = walletService;
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet("GetAllWalletDTOByUserIDAsync/{userId}")]
        public async Task<IEnumerable<WalletDTO>> GetAllWalletDTOByUserIDAsync(string userId)
        {
            var httpContext = httpContextAccessor.HttpContext;           
            if(httpContext.Request.Cookies.ContainsKey("UserIdCookie"))
            {
                var userIdJson = httpContext.Request.Cookies["UserIdCookie"];
                userId = JsonSerializer.Deserialize<string>(userIdJson);

                return await walletService.GetAllWalletDTOByUserId(userId);
            }
            else
            {
                return await walletService.GetAllWalletDTOByUserId(userId);
            }
            
        }

        [HttpPost("PostWalletDTOAsync")]
        public async Task<ActionResult> PostWalletDTOAsync(WalletDTO walletDTO)
        {
            try
            {
                walletDTO.Created_at = DateTime.UtcNow;
                walletDTO.Updated_at = DateTime.UtcNow;
                if(walletDTO != null)
                {
                
                    await walletService.CreateAsync(walletDTO);
                }
                else
                {
                    throw new Exception("Wallet invalid");
                }

                return Ok("Carteira criada com sucesso");
            }
            catch(Exception e)
            {
                return Ok(e.Message);
            }
        }
    

    }
}