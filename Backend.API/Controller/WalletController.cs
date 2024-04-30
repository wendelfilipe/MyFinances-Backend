using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Domain.Entites.WalletEntites;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService walletService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public WalletController(IWalletService walletService, IHttpContextAccessor httpContextAccessor)
        {
            this.walletService = walletService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAllWalletDTOByUserIDAsync/{userId}")]
        public async Task<IEnumerable<WalletDTO>> GetAllWalletDTOByUserIDAsync(int userId)
        {
            var httpContext = httpContextAccessor.HttpContext;           
            if(httpContext.Request.Cookies.ContainsKey("UserIdCookie"))
            {
                var userIdJson = httpContext.Request.Cookies["UserIdCookie"];
                userId = JsonSerializer.Deserialize<int>(userIdJson);

                return await walletService.GetAllWalletDTOByUserId(userId);
            }
            else
            {
                return await walletService.GetAllWalletDTOByUserId(userId);
            }
            
        }

        [HttpPost]
        public async Task PostWalletDTOAsync(WalletDTO walletDTO)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if(httpContext.Request.Cookies.ContainsKey("UserIdCookie"))
            {
                var userIdJson = httpContext.Request.Cookies["UserIdCookie"];
                var userId = JsonSerializer.Deserialize<int>(userIdJson);

                walletDTO.UserId = userId;
                await walletService.CreateAsync(walletDTO);
            }
            else
            {
                throw new Exception("Wallet invalid");
            }

        }
    

    }
}